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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Textures {
	public class Texture2D : Asset {
		/// <summary>
		/// Gets the texture ID.
		/// </summary>
		/// <value>The texture ID.</value>
		public int TextureID { get; private set; }
		public Vector2 Size { get; private set; }

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
			var image = loadImage(path, nearest);
			var tex = new Texture2D(image.Item2);
			tex.Size = image.Item1;
			return tex;
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

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

			image.UnlockBits(data);

			if (!nearest) {
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			} else {
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapNearest);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			}

			return texID;
		}

		public void Bind(TextureUnit unit) {
			GL.ActiveTexture(unit);
			GL.BindTexture(TextureTarget.Texture2D, this.TextureID);
		}

		/// <summary>
		/// Loads the image.
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="filename">The texture path.</param>
		/// <param name="nearest">If set to <c>true</c>, set filter to nearest.</param>
		private static Tuple<Vector2, int> loadImage(string filename, bool nearest = false) {
			try {
				Image file = Image.FromFile(filename);
				return new Tuple<Vector2, int>(new Vector2(file.Width, file.Height), loadImage(new Bitmap(file), nearest));
			} catch (FileNotFoundException) {
				return new Tuple<Vector2, int>(Vector2.Zero, -1);
			}
		}
	}

	public class Texture2DProvider : AssetProvider<Texture2D> {
		public Texture2DProvider(string root) 
			: base(root, "Textures/") {
		}

		public override Texture2D Load(string assetName, params object[] args) {
			if (args.Length < 1)
				return Texture2D.LoadTexture(assetName, false);
			return Texture2D.LoadTexture(assetName, (bool)args[0]);
		}
	}
}

