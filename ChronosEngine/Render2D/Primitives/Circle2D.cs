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
using OpenTK;
using ChronosEngine.Interfaces;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Render2D.Primitives {
	public class Circle2D : IRenderable2D {
		public Vector2 center;
		public Vector4 color;
		public float radius;

		public Circle2D(Vector2 center, float radius, Vector4 color) {
			this.center = center;
			this.radius = radius;
			this.color = color;
		}

		public void Render(IRenderer2D renderer) {
			GL.Begin(PrimitiveType.TriangleFan);

			GL.Vertex2(center);
			for (float ii = 0; ii < 361; ii++) { 
				float theta = 2.0f * 3.1415926f * ii / 360f;//get the current angle 

				float x = radius * (float)Math.Cos(theta);//calculate the x component 
				float y = radius * (float)Math.Sin(theta);//calculate the y component 

				GL.Vertex2(x + center.X, y + center.Y);//output vertex 

			} 
			GL.End(); 
		}
	}
}

