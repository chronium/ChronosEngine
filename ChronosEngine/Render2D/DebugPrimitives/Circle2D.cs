//
//  Circle2D.cs
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
using OpenTK;
using ChronosEngine.Interfaces;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Render2D.DebugPrimitives {
	public class Circle2D : IRenderable {
		public Vector2 center;
		public Vector4 color;
		public float radius;

		public Circle2D(Vector2 center, float radius, Vector4 color) {
			this.center = center;
			this.radius = radius;
			this.color = color;
		}

		public void Render() {
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

