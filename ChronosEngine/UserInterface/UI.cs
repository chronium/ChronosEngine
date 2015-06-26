using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Interfaces;
using ChronosEngine.Render2D;
using ChronosEngine.Textures;
using OpenTK;

namespace ChronosEngine.UserInterface {
	public class UI : IRenderable2D {
		public List<IElementUI> Elements = new List<IElementUI>();

		public UI() {
		}

		public void Render(IRenderer2D renderer) {
			foreach (IElementUI element in Elements)
				element.Render(renderer);
		}
	}
}
