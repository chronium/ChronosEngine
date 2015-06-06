﻿//
//  Sprite.cs
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
using ChronosEngine;

namespace ChronosEngine.Render2D {
	public class Sprite {
		/// <summary>
		/// The angle to rotate this Sprite around its center
		/// </summary>
		public float Rotation = 0.0f;

		/// <summary>
		/// The position of this Sprite in global space
		/// </summary>
		public Vector2 Position = new Vector2();

		/// <summary>
		/// The size of the Sprite, in pixels
		/// </summary>
		public Vector2 Scale = new Vector2(1.0f, 1.0f);

		/// <summary>
		/// The portion of the texture to use for this Sprite
		/// </summary>
		public RectangleF TexRect = new RectangleF(0.0f, 0.0f, 1.0f, 1.0f);

		/// <summary>
		/// The ID of the texture to use for this Sprite
		/// </summary>
		public int TextureID = -1;

		/// <summary>
		/// The last calculated model matrix of this Sprite
		/// </summary>
		public Matrix4 ModelMatrix = Matrix4.Identity;

		/// <summary>
		/// The last calculated MVP matrix for this Sprite
		/// </summary>
		public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

		private float maxDist = 1.0f;

		/// <summary>
		/// Gets or sets the size of this Sprite in pixels
		/// </summary>
		public SizeF Size {
			get {
				return new SizeF(Scale.X, Scale.Y);
			}
			set {
				Scale = new Vector2(value.Width, value.Height);
				maxDist = (float)Math.Sqrt(this.Scale.X * this.Scale.X + this.Scale.Y * this.Scale.Y);
			}
		}

		/// <summary>
		/// Calculates a model matrix for the transforms applied to this Sprite
		/// </summary>
		public void CalculateModelMatrix() {
			Vector3 translation = new Vector3();

			translation = new Vector3(Position.X - GameEngine.Instance.Window.GameResolution.Width / 2 - GameEngine.Instance.CurrentView.X, Position.Y - GameEngine.Instance.Window.GameResolution.Height / 2 - GameEngine.Instance.CurrentView.Y, 0.0f);

			ModelMatrix = Matrix4.CreateScale(Scale.X, Scale.Y, 1.0f) * Matrix4.CreateRotationZ(Rotation) * Matrix4.CreateTranslation(translation);
		}

		/// <summary>
		/// Creates a new Sprite
		/// </summary>
		/// <param name="textureID">The ID of the texture to draw on this Sprite</param>
		/// <param name="width">The width of the Sprite, in pixels</param>
		/// <param name="height">The height of the Sprite, in pixels</param>
		public Sprite(int textureID, int width, int height) {
			TextureID = textureID;
			Size = new Size(width, height);
		}

		/// <summary>
		/// Gets an array of vertices for the quad of this Sprite
		/// </summary>
		/// <returns></returns>
		public static Vector2[] GetVertices() {
			return new Vector2[]
			{
				new Vector2(-0.5f, -0.5f),
				new Vector2(-0.5f, 0.5f),
				new Vector2(0.5f, 0.5f),
				new Vector2(0.5f, -0.5f)
			};
		}

		/// <summary>
		/// Gets the texture coordinates for each vertice in the Sprite
		/// </summary>
		/// <returns></returns>
		public Vector2[] GetTexCoords() {
			return new Vector2[]
			{
				new Vector2(TexRect.Left, TexRect.Bottom),
				new Vector2(TexRect.Left, TexRect.Top),
				new Vector2(TexRect.Right, TexRect.Top),
				new Vector2(TexRect.Right, TexRect.Bottom)
			};
		}

		/// <summary>
		/// Gets the indices to draw this Sprite
		/// </summary>
		/// <param name="offset">Value to offset the indice values by (number of verts before this Sprite)</param>
		/// <returns>Array of indices to draw</returns>
		public int[] GetIndices(int offset = 0) {
			int[] indices = new int[] { 0, 1, 2, 0, 2, 3 };

			if (offset != 0) {
				for (int i = 0; i < indices.Length; i++) {
					indices[i] += offset;
				}
			}

			return indices;
		}

		/// <summary>
		/// Determines if this Sprite is visible in the current view
		/// </summary>
		public bool IsVisible {
			get {
				return Position.X + LongestSide > GameEngine.Instance.CurrentView.X && Position.X - LongestSide < GameEngine.Instance.CurrentView.X + GameEngine.Instance.CurrentView.Width && Position.Y + LongestSide > GameEngine.Instance.CurrentView.Y && Position.Y - LongestSide < GameEngine.Instance.CurrentView.Y + GameEngine.Instance.CurrentView.Height;
			}
		}

		/// <summary>
		/// Half the width of this Sprite
		/// </summary>
		public float HalfWidth { get { return Scale.X / 2.0f; } }

		/// <summary>
		/// Half the height of this Sprite
		/// </summary>
		public float HalfHeight { get { return Scale.Y / 2.0f; } }

		/// <summary>
		/// The length of the longest side of the rectangle drawn for this Sprite
		/// </summary>
		public float LongestSide { get { return Math.Max(Size.Width, Size.Height); } }

		/// <summary>
		/// The top-left corner of this Sprite
		/// </summary>
		public Vector2 TopLeft {
			get {
				return new Vector2((float)((-HalfWidth) * Math.Cos(Rotation) - (-HalfHeight) * Math.Sin(Rotation)), (float)((-HalfWidth) * Math.Sin(Rotation) + (-HalfHeight) * Math.Cos(Rotation))) + Position;
			}
		}

		/// <summary>
		/// The top-right corner of this Sprite
		/// </summary>
		public Vector2 TopRight {
			get {
				return new Vector2((float)((HalfWidth) * Math.Cos(Rotation) - (-HalfHeight) * Math.Sin(Rotation)), (float)((HalfWidth) * Math.Sin(Rotation) + (-HalfHeight) * Math.Cos(Rotation))) + Position;
			}
		}

		/// <summary>
		/// The bottom-left corner of this Sprite
		/// </summary>
		public Vector2 BottomLeft {
			get {
				return new Vector2((float)((-HalfWidth) * Math.Cos(Rotation) - (HalfHeight) * Math.Sin(Rotation)), (float)((-HalfWidth) * Math.Sin(Rotation) + (HalfHeight) * Math.Cos(Rotation))) + Position;
			}
		}

		/// <summary>
		/// The bottom-left corner of this Sprite
		/// </summary>
		public Vector2 BottomRight {
			get {
				return new Vector2((float)((HalfWidth) * Math.Cos(Rotation) - (HalfHeight) * Math.Sin(Rotation)), (float)((HalfWidth) * Math.Sin(Rotation) + (HalfHeight) * Math.Cos(Rotation))) + Position;
			}
		}

		/// <summary>
		/// Determine if a point is inside the Sprite's rotated rectangle
		/// </summary>
		/// <param name="point">Point to test</param>
		/// <returns>True if the given point is inside this Sprite's rectangle</returns>
		public bool IsInside(Vector2 point) {
			Vector2 AP = point - TopLeft;
			Vector2 AB = TopRight - TopLeft;
			Vector2 AD = BottomLeft - TopLeft;

			// Use the dot products to find if the point is inside or outside the Sprite
			return (0 < Vector2.Dot(AP, AB) && Vector2.Dot(AP, AB) < Vector2.Dot(AB, AB) && 0 < Vector2.Dot(AP, AD) && Vector2.Dot(AP, AD) < Vector2.Dot(AD, AD));
		}
	}
}

