using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Structures;
using OpenTK;

namespace CHIP8Emu {
	public class CHIP8 : ChronoGame {
		public int Scale { get; set; }
		public Resolution ScreenSize { get; set; }
		public Emulator Emulator { get; set; }

		private string rom;

		public CHIP8(string rom, int scale = 16, int width = 64, int height = 32) 
			: base(new Resolution(width * scale, height * scale), "CHIP8 Emulator") {
			this.Scale = scale;
			this.ScreenSize = new Resolution(width, height);

			this.rom = rom;
		}

		public override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.Content = new ContentManager("");
			this.Content.RegisterAssetProvider<Chip8ROM>(typeof(Chip8ROMProvider));
			this.Emulator = new Emulator(0xFFF, ScreenSize);

			Chip8ROM ROM = Content.Load<Chip8ROM>(rom);
			this.Emulator.WriteBytes(ROM.bytes, 0, 0x200, ROM.bytes.Length);
		}

		public override void OnUpdateFrame(FrameEventArgs e) {
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();
			this.SwapBuffers();
		}
	}
}
