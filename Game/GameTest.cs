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
using ChronosEngine;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using ChronosEngine.Interfaces;
using ChronosEngine.Render2D;
using System.Drawing;
using System.Collections.Generic;
using ChronosEngine.Scripting;
using ChronosEngine.Textures;
using ChronosEngine.UserInterface;
using ChronosEngine.Structures;

namespace Game {
	public class GameTest : ChronoGame {
		private IRenderer2D Renderer { get; set; }

		public List<IGameObject> GameObjects = new List<IGameObject>();

		public ScriptManager ScriptManager;

		public UI UserInterface;

		public GameTest() : base(new Resolution(800, 608), "Test game") {
			Renderer = new ImmediateRenderer2D();
			ScriptManager = new ScriptManager();
		}

		public override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			ScriptManager.AddScript("test");
			ScriptManager.LoadScripts();

			var buttonTexture = Texture2D.LoadTexture("button.png", true);
			UserInterface = new UI();
			UserInterface.Elements.Add(new UIButton(new Vector2(32, 96), Vector2.Zero, buttonTexture));
        }

		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();

			Renderer.Begin();
			foreach (IGameObject obj in GameObjects)
				obj.Render(Renderer);
			UserInterface.Render(Renderer);
			Renderer.End();

			this.SwapBuffers();
		}
	}
}

