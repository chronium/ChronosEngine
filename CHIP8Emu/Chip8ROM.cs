using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;

namespace CHIP8Emu {
	public class Chip8ROM : Asset {
		public byte[] rom;

		public Chip8ROM(byte[] rom) {
			this.rom = rom;
		}

		public static Chip8ROM LoadROM(string path) {
			if (File.Exists(path)) {
				byte[] bytes = null;
				using (var fs = new FileStream(path, FileMode.Open))
					fs.Read(bytes, 0, (int)fs.Length);
				return new Chip8ROM(bytes);
			}
			return null;
		}
	}

	public class Chip8ROMProvider : AssetProvider<Chip8ROM> {
		public Chip8ROMProvider(string root)
			: base(root, "roms/") {
		}

		public override Chip8ROM Load(string assetName, params object[] args) {
			return Chip8ROM.LoadROM(assetName);
		}
	}
}
