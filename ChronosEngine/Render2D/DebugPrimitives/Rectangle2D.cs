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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Render2D.DebugPrimitives {
	public class Rectangle2D : IRenderable2D {
		public Vector2 pos1, pos2;
		public Vector4 color;

		public Rectangle2D(Vector2 pos1, Vector2 pos2, Vector4 color) {
			this.pos1 = pos1;
			this.pos2 = pos2;
			this.color = color;
		}

		public void Render(IRenderer2D renderer) {
			GL.Begin(PrimitiveType.Quads);

			GL.Color4(color);
			GL.Vertex2(pos1);
			GL.Vertex2(pos2.X, pos1.Y);
			GL.Vertex2(pos2);
			GL.Vertex2(pos1.X, pos2.Y);

			GL.End();
		}
	}
}

