using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Structures;

namespace CHIP8Emu {
	public class Emulator {
		public Resolution Resolution { get; set; }
		public RAM RAM { get; set; }

		public byte[] V = new byte[16];
		public short I = 0;
		public byte DT = 0, ST = 0;
		public short PC = 0;
		public short SB = 0;
		public byte SP = 0;

		Random random = new Random();

		public Emulator(int ramSize, Resolution resolution) {
			this.RAM = new RAM(ramSize);
			Array.Clear(this.RAM.ram, 0, this.RAM.Length);
			this.Resolution = resolution;
			PC = 0x200;
			SB = 0xEA0;
        }

		public void WriteBytes(byte[] from, int start, int to, int length) {
			Array.Copy(from, start, this.RAM.ram, to, length);
		}

		public bool halted = false;
		public int keyReg = -1;

		public void keyPress(byte key) {
			if (halted && keyReg != -1) {
				V[keyReg] = key;
				halted = false;
			}
		}

		public void Run(CHIP8 chip) {
			if (!halted && DT <= 0) {
				ushort instr = (ushort)RAM.readShort(PC);
				PC += 2;
				byte op = (byte)((instr & 0xF000) >> 12);
				byte x = (byte)((instr & 0x0F00) >> 8);
				byte y = (byte)((instr & 0x00F0) >> 4);
				byte n = (byte)((instr & 0x000F));
				byte kk = (byte)((instr & 0x00FF));
				short nnn = (short)((instr & 0x0FFF));

				short temp = 0;

				switch ((instr & 0xF000) >> 12) {
					case 0x0:
						switch (kk) {
							case 0xEE:
								PC = RAM[SB + SP];
								SP -= 2;
								break;
							case 0xE0:
								Array.Clear(chip.Screen, 0, chip.Screen.Length);
								break;
							default:
								Console.WriteLine("Unknown opcode: {0}", instr.ToString("x").PadLeft(4, '0'));
								while (true) ;
						}
						break;
					case 0x1:
						PC = nnn;
						break;
					case 0x2:
						SP += 2;
						RAM.writeShort(SB + SP, PC);
						PC = nnn;
						break;
					case 0x3:
						if (V[x] == kk)
							PC += 2;
						break;
					case 0x4:
						if (V[x] != kk)
							PC += 2;
						break;
					case 0x5:
						if (V[x] == V[y])
							PC += 2;
						break;
					case 0x6:
						V[x] = kk;
						break;
					case 0x7:
						V[x] += kk;
						break;
					case 0x8:
						switch(n) {
							case 0x0:
								V[x] = V[y];
								break;
							case 0x1:
								V[x] |= V[y];
								break;
							case 0x2:
								V[x] &= V[y];
								break;
							case 0x3:
								V[x] ^= V[y];
								break;
							case 0x4:
								temp = (short)(V[x] + V[y]);
								V[0xF] = (byte)(temp > 0xFF ? 1 : 0);
								V[x] = (byte)(temp & 0xFF);
								break;
							case 0x5:
								temp = (short)(V[x] - V[y]);
								V[0xF] = (byte)(V[x] > V[y] ? 1 : 0);
								V[x] = (byte)(temp & 0xFF);
								break;
							case 0x6:
								V[0xF] = (byte)(V[x] & 1);
								V[x] >>= 1;
								break;
							case 0x7:
								V[0xF] = (byte)((V[x] & 128) >> 7);
								V[x] <<= 1;
								break;
							default:
								Console.WriteLine("Unknown opcode: {0}", instr.ToString("x").PadLeft(4, '0'));
								while (true) ;
						}
						break;
					case 0x9:
						if (V[x] != V[y])
							PC += 2;
						break;
					case 0xA:
						I = nnn;
						break;
					case 0xB:
						PC = (short)(nnn + V[0x0]);
						break;
					case 0xC:
						byte r = (byte)random.Next(0, 0xFF);
						V[x] = (byte)(r & kk);
						break;
					case 0xD:
						for (int yl = V[y]; yl < V[y] + n; yl++)
							for (int xl = V[x]; xl < V[x] + 8; xl++) {
								int b = 8 - (xl - V[x]);
								byte set = (byte)(RAM[I + (yl - V[y])] & (1 << b - 1));
								if (xl < 0 || xl >= chip.ScreenSize.Width)
									continue;
								if (yl < 0 || yl >= chip.ScreenSize.Height)
									continue;
								if (chip.Screen[xl + yl * chip.ScreenSize.Width] == 1 && set == 1)
									V[0xF] = 1;
								else 
									V[0xF] = 0;

								chip.Screen[xl + yl * chip.ScreenSize.Width] = set;
                            }
						break;
					case 0xE:
						switch(kk) {
							case 0x9E:
								if (chip.Keys[V[x]])
									PC += 2;
								break;
							case 0xA1:
								if (!chip.Keys[V[x]])
									PC += 2;
								break;
							default:
								Console.WriteLine("Unknown opcode: {0}", instr.ToString("x").PadLeft(4, '0'));
								while (true) ;
						}
						break;
					case 0xF:
						switch(kk) {
							case 0x07:
								V[x] = DT;
								break;
							case 0x0A:
								halted = true;
								keyReg = x;
								break;
							case 0x15:
								DT = V[x];
								break;
							case 0x18:
								ST = V[x];
								break;
							case 0x1E:
								I += V[x];
								break;
							case 0x29:
								I = (short)(V[x] * 5);
								break;
							case 0x33:
								RAM[I] = (byte)(V[x] / 100);
								RAM[I + 1] = (byte)((V[x] / 10) % 10);
								RAM[I + 2] = (byte)(V[x] % 10);
								break;
							case 0x55:
								for (int i = 0; i <= x; i++)
									RAM[I + i] = V[i];
								break;
							case 0x65:
								for (int i = 0; i <= x; i++)
									V[i] = RAM[I + i];
								break;
							default:
								Console.WriteLine("Unknown opcode: {0}", instr.ToString("x").PadLeft(4, '0'));
								while (true) ;
						}
						break;
					default:
						Console.WriteLine("Unknown opcode: {0}", instr.ToString("x").PadLeft(4, '0'));
						while (true) ;
				}
			}
		}
	}
}
