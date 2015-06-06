//
//  Texture2D.cs
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
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace ChronosEngine.Textures {
	public class Texture2D {
		/// <summary>
		/// Gets the texture ID.
		/// </summary>
		/// <value>The texture ID.</value>
		public int TextureID { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.Textures.Texture2D"/> class.
		/// </summary>
		/// <param name="textureID">Texture ID.</param>
		public Texture2D(int textureID) {
			this.TextureID = textureID;
		}

		/// <summary>
		/// Loads a 2D texture.
		/// </summary>
		/// <returns>A <see cref="ChronosEngine.Textures.Texture2D"/> instance that points to the specified texture.</returns>
		/// <param name="path">The image path.</param>
		/// <param name="nearest">If set to <c>true</c>, set filter to nearest.</param>
		public static Texture2D LoadTexture(string path, bool nearest = false) {
			return new Texture2D(loadImage(String.Format("Assets/Textures/{0}", path), nearest));
		}

		/// <summary>
		/// Loads a texture from bitmap
		/// </summary>
		/// <returns>The itexture.</returns>
		/// <param name="image">Image.</param>
		/// <param name="nearest">If set to <c>true</c>, set filter to nearest.</param>
		private static int loadImage(Bitmap image, bool nearest = false) {
			int texID = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texID);
			BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
				                  ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			image.UnlockBits(data);

			if (!nearest) {
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			}
			else {
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapNearest);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			}
			
			return texID;
		}

		/// <summary>
		/// Loads the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="filename">The texture path.</param>
		/// <param name="nearest">If set to <c>true</c>, set filter to nearest.</param>
		private static int loadImage(string filename, bool nearest = false) {
			try {
				Image file = Image.FromFile(filename);
				return loadImage(new Bitmap(file), nearest);
			}
			catch (FileNotFoundException e) {
				return -1;
			}
		}
	}
}

