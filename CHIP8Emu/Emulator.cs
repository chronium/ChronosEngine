using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Structures;

namespace CHIP8Emu {
	public class Emulator {
		public byte[] RAM { get; set; }
		public Resolution Resolution { get; set; }
				
		public Emulator(int ramSize, Resolution resolution) {
			this.RAM = new byte[ramSize];
			Array.Clear(this.RAM, 0, this.RAM.Length);
			this.Resolution = resolution;
		}

		public void WriteBytes(byte[] from, int start, int to, int length) {
			Array.Copy(from, start, this.RAM, to, length);
		}
	}
}
