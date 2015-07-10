using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP8Emu {
	public class RAM {
		public byte[] ram { get; set; }
		public int Length { get; set; }

		public byte this[int addr] {
			get {
				return ram[addr];
			}
			set {
				ram[addr] = value;
			}
		}

		public RAM(int ramSize) {
			this.ram = new byte[ramSize];
			this.Length = ramSize;
		}

		public short readShort(int address) {
			return (short)((ram[address] << 8) | ram[address + 1]);
		}

		public void writeShort(int address, short value) {
			ram[address] = (byte)((value >> 8) & 0xFF);
			ram[address + 1] = (byte)(value & 0xFF);
		}
	}
}
