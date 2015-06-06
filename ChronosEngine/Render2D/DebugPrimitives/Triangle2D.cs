//
//  Triangle2D.cs
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
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Render2D.DebugPrimitives {
	public class Triangle2D : IRenderable {
		public Vector2 v1, v2, v3;
		public Vector4 color;

		public Triangle2D(Vector2 v1, Vector2 v2, Vector2 v3, Vector4 color) {
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
			this.color = color;
		}

		public void Render() {
			GL.Begin(PrimitiveType.Triangles);

			GL.Color4(color);
			GL.Vertex2(v1);
			GL.Vertex2(v2);
			GL.Vertex2(v3);

			GL.End();
		}
	}
}

