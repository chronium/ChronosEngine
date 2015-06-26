using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Interfaces;
using ChronosEngine.Textures;
using OpenTK;

namespace ChronosEngine.UserInterface {
	public class UIButton : IElementUI {
		public Vector2 Size;
		public Vector2 Position;
		public Texture2D Texture;

		public UIButton(Vector2 size, Vector2 position, Texture2D texture) {
			this.Size = size;
			this.Position = position;
			this.Texture = texture;
		}

		public void Render(IRenderer2D renderer) {
			renderer.Draw(Texture, Position, Size);
		}
	}
}
