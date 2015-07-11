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
using OpenTK.Input;

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
			this.Emulator.WriteBytes(RAM.font, 0, 0, RAM.font.Length);
			this.Emulator.WriteBytes(ROM.bytes, 0, 0x200, ROM.bytes.Length);
		}

		public int count = 0;
		public bool[] Keys { get; set; } = new bool[16];
		private char[] keyCodes = new char[] { '1', '2', '3', '4',
											   'q', 'w', 'e', 'r',
											   'a', 's', 'd', 'f',
                                               'z', 'x', 'c', 'v' };

		public override void OnKeyDown(KeyboardKeyEventArgs e) {
			for (int i = 0; i < keyCodes.Length; i++)
				if (e.Key.ToString().ToLower()[0] == keyCodes[i])
					Keys[i] = true;
		}
		public override void OnKeyUp(KeyboardKeyEventArgs e) {
			for (int i = 0; i < keyCodes.Length; i++)
				if (e.Key.ToString().ToLower()[0] == keyCodes[i])
					Keys[i] = false;
		}

		public override void OnKeyPress(KeyPressEventArgs e) {
			for (int i = 0; i < keyCodes.Length; i++) {
				if (e.KeyChar == keyCodes[i]) {
					Keys[i] = true;
					Emulator.keyPress((byte)i);
				} else
					Keys[i] = false;
			}
		}

		public override void OnUpdateFrame(FrameEventArgs e) {
			Emulator.Run(this);
			count++;
			GameEngine.Window.Title = "CHIP8 Emulator - Cycles: " + count;
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();

			if (Emulator.DT > 0)
				Emulator.DT--;

			if (Emulator.ST > 0) {
				Emulator.ST--;
				if (Emulator.ST == 0)
					Console.Beep();
			}

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
			x -= (ScreenSize.Width / 2);
			y -= (ScreenSize.Height / 2) + 1;
			Rectangle2D.Render(new Vector2(x++ * Scale, y++ * Scale), new Vector2(x * Scale, y * Scale), new Vector4(1));
		}
	}
}
