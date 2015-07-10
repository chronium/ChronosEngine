using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Interfaces;
using ChronosEngine.Render2D;
using ChronosEngine.Render2D.Primitives;
using ChronosEngine.Structures;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CHIP8Emu {
	public class CHIP8 : ChronoGame {
		private IRenderer2D renderer;

		public int Scale { get; set; }
		public Resolution ScreenSize { get; set; }
		public Emulator Emulator { get; set; }

		private string rom;

		public byte[] Screen { get; set; }

		public CHIP8(string rom, int scale = 16, int width = 64, int height = 32) 
			: base(new Resolution(width * scale, height * scale), "CHIP8 Emulator") {
			this.Scale = scale;
			this.ScreenSize = new Resolution(width, height);
			this.Screen = new byte[width * height];

			this.rom = rom;
		}

		public override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.Content = new ContentManager("");
			this.Content.LoadPredefinedProviders();
			this.Content.RegisterAssetProvider<Chip8ROM>(typeof(Chip8ROMProvider));
			this.Emulator = new Emulator(0xFFF, ScreenSize);

			this.renderer = new ImmediateRenderer2D();

			Chip8ROM ROM = Content.Load<Chip8ROM>(rom);
			this.Emulator.WriteBytes(ROM.bytes, 0, 0x200, ROM.bytes.Length);
		}

		public override void OnKeyPress(KeyPressEventArgs e) {
			if (e.KeyChar == ' ')
				Emulator.Run(this);
		}

		public override void OnUpdateFrame(FrameEventArgs e) {
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();

			renderer.Begin();
			for (int y = 0; y < ScreenSize.Height; y++)
				for (int x = 0; x < ScreenSize.Width; x++) {
					if (Screen[x + y * ScreenSize.Width] != 0)
						DrawPixel(x, ScreenSize.Height - y);
				}
			renderer.End();

			this.SwapBuffers();
		}

		public void DrawPixel(int x, int y) {
			x -= (ScreenSize.Width / 2) - 7;
			y -= (ScreenSize.Height / 2) - 2;
			Rectangle2D.Render(new Vector2(x++ * Scale, y++ * Scale), new Vector2(x * Scale, y * Scale), new Vector4(1));
		}
	}
}
