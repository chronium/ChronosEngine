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

		public CHIP8(int scale = 16, int width = 64, int height = 32) 
			: base(new Resolution(width * scale, height * scale), "CHIP8 Emulator") {
			this.Scale = scale;
			this.ScreenSize = new Resolution(width, height);
		}

		public override void OnUpdateFrame(FrameEventArgs e) {
			base.OnUpdateFrame(e);
		}

		public override void OnLoad(EventArgs e) {
			this.Clear();
			this.SwapBuffers();
		}
	}
}
