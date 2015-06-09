//
//  Author:
//    Chronium Silver (Andrei Dimitriu) onlivechronium@gmail.com
//
//  Copyright (c) 2015, Chronium @ ChronoStudios
//
//  All rights reserved.
//
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
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
		public void Begin() {
			GL.PushMatrix();
			GL.LoadMatrix(ref DefaultGlobals.OrthographicProjection);
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

