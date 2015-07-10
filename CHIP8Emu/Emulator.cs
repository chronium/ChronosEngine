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
				case 0x2:
					short jmpAddr = (short)(instr & 0x0FFF);
                    Console.WriteLine("Jump to: " + jmpAddr.ToString("x"));
					RAM.writeShort(SB + (SP * 2), PC);
					PC = jmpAddr;
					break;
				case 0x6:
					byte reg = (byte)((instr & 0x0F00) >> 8);
					byte val = (byte)(instr & 0x00FF);
					Console.WriteLine("Setting register {0} to {1}", reg.ToString("x"), val.ToString("x"));
					V[reg] = val;
					break;
				case 0xA:
					short setVal = (short)(instr & 0x0FFF);
					Console.WriteLine("Set reg I to: " + setVal.ToString("x"));
					I = setVal;
					break;
				case 0xD:
					byte xr = (byte)((instr & 0x0F00) >> 8);
					byte yr = (byte)((instr & 0x00F0) >> 4);
					byte s = (byte)(instr & 0x000F);
					Console.WriteLine("Drawing {0} bytes starting at {1} at coords ({2},{3})", s, I.ToString("x"), V[xr], V[yr]);
					for (int y = V[yr]; y < V[yr] + s; y++)
						for (int x = V[xr]; x < V[xr] + 8; x++) {
							bool set = (RAM[I + (y - V[yr])] & (1 << (x - V[x]))) != 0;
                            if ((chip.Screen[x + y * chip.ScreenSize.Width] > 1) == set) {
								V[0xF] = 1;
								chip.Screen[x + y * chip.ScreenSize.Width] = 0;
							} else {
								V[0xF] = 0;
								chip.Screen[x + y * chip.ScreenSize.Width] = 1;
							}
						}
					break;
				case 0x7:
					reg = (byte)((instr & 0x0F00) >> 8);
					val = (byte)(instr & 0x00FF);
					Console.WriteLine("Reg {0} += {1}", reg.ToString("x"), val.ToString("x"));
					V[reg] += val;
					break;
			}
		}
	}
}
