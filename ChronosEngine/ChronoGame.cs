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
using ChronosEngine.Structures;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine {
	public class ChronoGame {
		/// <summary>
		/// Gets the game engine.
		/// </summary>
		/// <value>The game engine.</value>
		public GameEngine GameEngine { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.ChronoGame"/> class.
		/// </summary>
		/// <param name="title">Window title.</param>
		public ChronoGame(string title = "Untitled") {
			this.GameEngine = new GameEngine(new Resolution(800, 600), new Resolution(800, 600), title, this);
			DefaultGlobals.OrthographicProjection = Matrix4.CreateOrthographic(GameEngine.GameResolution.Width, -GameEngine.GameResolution.Height, 64f, -64f);
		}

		/// <summary>
		/// Setup OpenGL and load resources here.
		/// </summary>
		/// <param name="e">Not used.</param>
		public virtual void OnLoad(EventArgs e) {
			this.SetClearColor(Color.Black);
			this.SetViewport(0, 0, GameEngine.GameResolution.Width, GameEngine.GameResolution.Height);
			this.EnableTextures();
			this.SetBlendMode();
		}
		/// <summary>
		/// Respond to resize events here.
		/// </summary>
		/// <param name="e">Contains information on the new GameWindow size.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		public virtual void OnResize(EventArgs e) { }
		/// <summary>
		/// Add your game logic here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		public virtual void OnUpdateFrame(FrameEventArgs e) { }
		/// <summary>
		/// Add your game rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		public virtual void OnRenderFrame(FrameEventArgs e) { }

		public void SetClearColor(Color color) {
			GL.ClearColor(color);
		}
		public void SetViewport(int x, int y, int width, int height) {
			GL.Viewport(x, y, width, height);
		}
		public void EnableTextures() {
			GL.Enable(EnableCap.Texture2D);
		}
		public void SetBlendMode() {
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		public void Clear() {
			GL.Clear(ClearBufferMask.ColorBufferBit);
		}

		public void SwapBuffers() {
			GameEngine.Window.SwapBuffers();
		}
	}
}

