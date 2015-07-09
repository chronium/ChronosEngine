using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP8Emu {
	public class Emulator {
		public byte[] RAM;

		public Emulator(int ramSize) {
			RAM = new byte[ramSize];
			Array.Clear(RAM, 0, RAM.Length);
		}

		public void WriteBytes(byte[] from, int start, int to, int length) {
			Array.Copy(from, start, RAM, to, length);
		}
	}
}
