using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Base;

namespace CHIP8Emu {
	public class Chip8ROM : Asset {
		public byte[] bytes;

		public Chip8ROM(byte[] rom) {
			this.bytes = rom;
		}

		public static byte[] ReadFile(string filePath) {
			byte[] buffer;
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			try {
				int length = (int)fileStream.Length;  
				buffer = new byte[length];            
				int count;                            
				int sum = 0;                          
				
				while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
					sum += count;
			} finally {
				fileStream.Close();
			}
			return buffer;
		}

		public static Chip8ROM LoadROM(string path) {
			if (File.Exists(path)) {
				return new Chip8ROM(ReadFile(path));
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
