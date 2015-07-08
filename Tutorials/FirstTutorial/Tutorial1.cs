using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using OpenTK;

namespace Tutorials.FirstTutorial {
	// Inherit from the base class ChronoGame
	public class Tutorial1: ChronoGame {
		// Required call to the base constructor. 
		// It sets up the window and an orthographic projection.
		public Tutorial1() : base("Tutorial 1 - First window") { }

		public override void OnLoad(EventArgs e) {
			// The base function generates an asset context and a rendering context.
			base.OnLoad(e);
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			// We clear the back buffer to have a clean frame.
			this.Clear();
			// We swap the back and front buffer to present our frame to the user.
			this.SwapBuffers();
		}
	}
}
