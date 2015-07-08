using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Helpers;
using ChronosEngine.Interfaces;
using ChronosEngine.Render2D;
using ChronosEngine.Render2D.Primitives;
using OpenTK;

namespace Tutorials.SecondTutorial {
	// Inherit from the base class ChronoGame
	class Tutorial2 : ChronoGame {
		// We make a field for a 2D renderer
		private IRenderer2D renderer;

		// We make a new list for our primitives
		private List<IRenderable2D> Primitives { get; set; }

		// Required call to the base constructor. 
		// It sets up the window and an orthographic projection.
		public Tutorial2() : base("Tutorial 2 - Drawing Primitives") { }

		public override void OnLoad(EventArgs e) {
			// The base function generates an asset context and a rendering context.
			base.OnLoad(e);
			// We initialize our renderer to an immediate renderer
			renderer = new ImmediateRenderer2D();

			// We initialize the primitives list
			Primitives = new List<IRenderable2D>();

			// We convert a color from hex to 0-1 float color
			Vector4 blue = ColorHelper.HexToVector4(0x00, 0x33, 0x99, 0xFF);
			// Coordinates go from (-width)-(-height) to with-height.
			// (-width)-(-height) being bottom left and width-height being top right
			Primitives.Add(new Rectangle2D(
				new Vector2(-240, -120),
				new Vector2(240, 120),
				blue));
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			// We clear the back buffer to have a clean frame.
			this.Clear();

			// We initialize our renderer
			renderer.Begin();

			// We itterate through every primitive we have
			foreach (IRenderable2D primitive in Primitives)
				/// We render our primitive using our renderer
				primitive.Render(renderer);
			
			// We send the data to the graphics card.
			renderer.End();

			// We swap the back and front buffer to present our frame to the user.
			this.SwapBuffers();
		}
	}
}
