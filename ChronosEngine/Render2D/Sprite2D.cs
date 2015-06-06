//
//  Sprite2D.cs
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
using System.Drawing;
using ChronosEngine.Textures;

namespace ChronosEngine.Render2D {
	public class Sprite2D {
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }
		public RectangleF TextureCoords { get; set; }
		public Texture2D Texture { get; set; }

		public Sprite2D(Vector2 position, Vector2 size) {
			this.Position = position;
			this.Size = size;
			this.TextureCoords = RectangleF.Empty;
			this.Texture = null;
		}

		public Sprite2D(Vector2 position, Vector2 size, RectangleF texCoords, Texture2D texture)
			: this(position, size) {
			this.TextureCoords = texCoords;
			this.Texture = texture;
		}
	}
}

