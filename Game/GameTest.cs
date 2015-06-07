﻿//
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

namespace Game {
	public class GameTest : ChronoGame {
		private IRenderer2D Renderer { get; set; }
		private Matrix4 Orthographic;

		public List<IGameObject> GameObjects = new List<IGameObject>();

		public ScriptManager ScriptManager;

		public GameTest() : base() {
			Renderer = new ImmediateRenderer2D();
			Orthographic = Matrix4.CreateOrthographic(GameEngine.GameResolution.Width, -GameEngine.GameResolution.Height, 64f, -64f);
		
			ScriptManager = new ScriptManager();
		}

		public override void OnLoad(EventArgs e) {
			GL.ClearColor(Color.CornflowerBlue);
			GL.Viewport(0, 0, GameEngine.GameResolution.Width, GameEngine.GameResolution.Height);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			ScriptManager.AddScript("test");

			ScriptManager.LoadScripts();

			GameObjects.Add(new Sprite2D(Vector2.Zero, new Vector2(32, 32), true));
		}

		public override void OnRenderFrame(FrameEventArgs e) {
			GL.Clear(ClearBufferMask.ColorBufferBit);

			Renderer.Begin(ref Orthographic);
			foreach (IGameObject obj in GameObjects)
				obj.Render(Renderer);
			Renderer.End();

			GameEngine.Window.SwapBuffers();
		}
	}
}
