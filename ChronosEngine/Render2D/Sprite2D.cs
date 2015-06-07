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
using System.Drawing;
using ChronosEngine.Textures;
using ChronosEngine.Interfaces;

namespace ChronosEngine.Render2D {
	public class Sprite2D : IGameObject {
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }
		public RectangleF TextureCoords { get; set; }
		public Texture2D Texture { get; set; }
		public bool Centered { get; set; }

		public Sprite2D(Vector2 position, Vector2 size, bool centered = false) {
			this.Position = position;
			this.Size = size;
			this.TextureCoords = RectangleF.Empty;
			this.Texture = null;
			this.Centered = false;
		}

		public Sprite2D(Vector2 position, Vector2 size, RectangleF texCoords, Texture2D texture, bool centered = false)
			: this(position, size, centered) {
			this.TextureCoords = texCoords;
			this.Texture = texture;
		}

		public void Render(IRenderer2D renderer) {
			renderer.Draw(this, Centered);
		}
	}
}

