﻿//
//  Line2D.cs
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
	public class Line2D : IRenderable {
		public Vector2 pos1, pos2;
		public Vector4 color;
		public float width;

		public Line2D(Vector2 pos1, Vector2 pos2, Vector4 color, float width) {
			this.pos1 = pos1;
			this.pos2 = pos2;
			this.color = color;
			this.width = width;
		}

		public void Render() {
			GL.LineWidth(width);
			GL.Begin(PrimitiveType.Lines);

			GL.Color4(color);
			GL.Vertex2(pos1);
			GL.Vertex2(pos2);
	
			GL.End();
		}
	}
}

