using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using OpenTK;

namespace Tutorials.FirstTutorial {
	public class Tutorial1: ChronoGame {
		public Tutorial1() : base("Tutorial 1 - First window") { }

		public override void OnLoad(EventArgs e) {
			base.OnLoad(e);
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();
			this.SwapBuffers();
		}
	}
}
