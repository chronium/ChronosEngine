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
using ChronosEngine.Structures;
using System.Collections.Generic;
using ChronosEngine.Shaders;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using ChronosEngine.Textures;
using ChronosEngine.Render2D;
using ChronosEngine.Interfaces;
using ChronosEngine.Scripting;

namespace ChronosEngine {
	public class GameEngine2D {
		private Matrix4 Orthographic;

		private Resolution GameResolution { get; set; }

		private IRenderer2D Renderer { get; set; }

		private List<IGameObject> GameObjects { get; set; }

		Texture2D texture;

		public GameEngine2D(Resolution gameResolution) {
			this.GameResolution = gameResolution;

			Orthographic = Matrix4.CreateOrthographic(GameResolution.Width, -GameResolution.Height, 64f, -64f);
			Renderer = new ImmediateRenderer2D();
			GameObjects = new List<IGameObject>();
		}

		public void Load(EventArgs e) {
			GL.ClearColor(Color.CornflowerBlue);
			GL.Viewport(0, 0, GameResolution.Width, GameResolution.Height);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			var addObj = new Action<IGameObject>(AddObject);

			foreach (Script script in GameEngine.Instance.Scripts)
				script.ScriptScope.SetVariable("add_object", addObj);

			texture = Texture2D.LoadTexture("platform1.png", true);
			GameObjects.Add(new Sprite2D(new Vector2(0, 0), new Vector2(32, 32), new RectangleF(0, 0, 1, 1), texture, true));
			GameObjects.Add(new Sprite2D(new Vector2(64, 0), new Vector2(16, 32), true));
		}

		public void AddObject(IGameObject obj) {
			GameObjects.Add(obj);
		}

		public void Resize(EventArgs e) {
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
		}

		public void Update(FrameEventArgs e) {
		}

		public void Render(FrameEventArgs e) {
			Renderer.Begin(ref Orthographic);
			foreach (IGameObject gameObject in GameObjects)
				gameObject.Render(Renderer);
			Renderer.End();
		}
	}
}

