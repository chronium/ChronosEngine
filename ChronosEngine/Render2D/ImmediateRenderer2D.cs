//
//  ImmediateRenderer2D.cs
//
//  Author:
//       Chronium Silver (Andrei Dimitriu) <onlivechronium@gmail.com>
//
//  Copyright (c) 2015 Chronium @ ChronoStudios
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using ChronosEngine.Interfaces;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace ChronosEngine.Render2D {
	public class ImmediateRenderer2D : IRenderer2D {
		public void Begin(ref Matrix4 renderMatrix) {
			GL.PushMatrix();
			GL.LoadMatrix(ref renderMatrix);
		}

		public void Draw(Sprite2D sprite, bool centered = false) {
			Vector2 p = sprite.Position;
			Vector2 s = sprite.Size;

			if (centered)
				p -= s / 2;

			GL.Begin(PrimitiveType.Quads);
			GL.BindTexture(TextureTarget.Texture2D, 0);
			if (sprite.Texture == null) {
				Ver2(new Vector2(p.X, p.Y));
				Ver2(new Vector2(p.X + s.X, p.Y));
				Ver2(new Vector2(p.X + s.X, p.Y + s.Y));
				Ver2(new Vector2(p.X, p.Y + s.Y));
			} else {
				RectangleF tc = sprite.TextureCoords;

				GL.BindTexture(TextureTarget.Texture2D, sprite.Texture.TextureID);
				Ver2Tex2(new Vector2(p.X, p.Y), new Vector2(tc.X, tc.Y));
				Ver2Tex2(new Vector2(p.X + s.X, p.Y), new Vector2(tc.X + tc.Width, tc.Y));
				Ver2Tex2(new Vector2(p.X + s.X, p.Y + s.Y), new Vector2(tc.X + tc.Width, tc.Y + tc.Height));
				Ver2Tex2(new Vector2(p.X, p.Y + s.Y), new Vector2(tc.X, tc.Y + tc.Height));
			}
			GL.End();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void Ver2Tex2(Vector2 v, Vector2 t) {
			GL.TexCoord2(t);
			GL.Vertex2(v);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void Ver2(Vector2 v) {
			GL.Vertex2(v);
		}

		public void End() {
			GL.PopMatrix();
		}
	}
}

