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

		public void Run(CHIP8 chip) {
			short instr = RAM.readShort(PC);
			PC += 2;
			switch ((instr & 0xF000) >> 12) {
				case 0x0:
					if (instr == 0x00EE) {
						SP -= 2;
						PC = RAM.readShort(SB + (SP * 2));
					}
					break;
				case 0x1:
					short jmpAddr = (short)(instr & 0x0FFF);
					PC = jmpAddr;
					break;
				case 0x2:
					jmpAddr = (short)(instr & 0x0FFF);
					RAM.writeShort(SB + (SP * 2), PC);
					SP += 2;
					PC = jmpAddr;
					break;
				case 0x3:
					byte reg = (byte)((instr & 0x0F00) >> 8);
					byte val = (byte)(instr & 0x00FF);
					if (V[reg] == val)
						PC += 2;
					break;
				case 0x4:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					if (V[reg] != val)
						PC += 2;
					break;
				case 0x6:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					V[reg] = val;
					break;
				case 0x7:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					V[reg] += val;
					break;
				case 0xA:
					short setVal = (short)(instr & 0x0FFF);
					I = setVal;
					break;
				case 0xC:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					V[reg] = (byte)(random.Next(0, 0xFF) & val);
					break;
				case 0xD:
					byte xr = (byte)((instr & 0x0F00) >> 8);
					byte yr = (byte)((instr & 0x00F0) >> 4);
					byte s = (byte)(instr & 0x000F);
					for (int y = V[yr]; y < V[yr] + s; y++)
						for (int x = V[xr]; x < V[xr] + 8; x++) {
							int n = 8 - (x - V[xr]);
							byte set = (byte)(RAM[I + (y - V[yr])] & (1 << n - 1));
							if (x < 0 || x >= chip.ScreenSize.Width)
								continue;
							if (y < 0 || y >= chip.ScreenSize.Height)
								continue;
							if (chip.Screen[x + y * chip.ScreenSize.Width] == set) {
								V[0xF] = 1;
								chip.Screen[x + y * chip.ScreenSize.Width] = 0;
							} else {
								V[0xF] = 0;
								chip.Screen[x + y * chip.ScreenSize.Width] = 1;
							}
						}
					break;
				case 0xE:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					switch (val) {
						case 0xE9:
							if (chip.Keys[V[reg]])
								PC += 2;
							break;
						case 0xA9:
							if (!chip.Keys[V[reg]])
								PC += 2;
							break;
					}
					break;
				case 0xF:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					switch (val) {
						case 0x1E:
							I += V[reg];
							break;
						default:
							Console.WriteLine("Unknown: {0} at cycle {1}", instr.ToString("x").PadLeft(4, '0'), chip.count);
							while (true) ;
					}
					break;
				default:
					Console.WriteLine("Unknown: {0} at cycle {1}", instr.ToString("x").PadLeft(4, '0'), chip.count);
					while (true) ;
			}
		}
	}
}
