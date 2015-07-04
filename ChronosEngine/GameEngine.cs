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
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using OpenTK.Graphics;
using ChronosEngine.Structures;
using ChronosEngine.Render2D.DebugPrimitives;
using ChronosEngine.Shaders;
using Microsoft.Scripting.Hosting;
using System.Collections.Generic;
using ChronosEngine.Scripting;
using System.IO;

namespace ChronosEngine {
	public class GameEngine {
		/// <summary>
		/// Gets the window.
		/// </summary>
		/// <value>The window.</value>
		public Window Window { get; private set; }

		/// <summary>
		/// Gets or sets the game resolution.
		/// </summary>
		/// <value>The game resolution.</value>
		public Resolution GameResolution { get; set; }

		/// <summary>
		/// The current view.
		/// </summary>
		public RectangleF CurrentView = new RectangleF(0, 0, 800, 600);

		public ChronoGame Game { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.GameEngine"/> class.
		/// </summary>
		/// <param name="screenResolution">Screen resolution.</param>
		/// <param name="gameResolution">Game resolution.</param>
		/// <param name="windowTitle">Window title.</param>
		public GameEngine(Resolution screenResolution, Resolution gameResolution, string windowTitle, ChronoGame game) {
			this.GameResolution = gameResolution;

			Window = new Window(screenResolution, gameResolution, windowTitle);
			Window.KeyDown += Keyboard_KeyDown;
			Window.Load += OnLoad;
			Window.Resize += OnResize;
			Window.RenderFrame += OnRenderFrame;
			Window.UpdateFrame += OnUpdateFrame;
			Window.MouseMove += OnMouseMove;
			Game = game;
		}

		#region Keyboard_KeyDown

		/// <summary>
		/// Occurs when a key is pressed.
		/// </summary>
		/// <param name="sender">The KeyboardDevice which generated this event.</param>
		/// <param name="e">The key that was pressed.</param>
		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e) {
			if (e.Key == Key.Escape)
				Window.Exit();

			if (e.Key == Key.F11)
			if (Window.WindowState == WindowState.Fullscreen)
				Window.WindowState = WindowState.Normal;
			else
				Window.WindowState = WindowState.Fullscreen;
		}

		#endregion

		#region OnLoad

		/// <summary>3
		/// Setup OpenGL and load resources here.
		/// </summary>
		/// <param name="sender">Caller of the function</param>
		/// <param name="e">Not used.</param>
		public void OnLoad(object sender, EventArgs e) {
			Game.OnLoad(e);
		}

		#endregion

		#region OnResize

		/// <summary>
		/// Respond to resize events here.
		/// </summary>
		/// <param name="sender">Caller of the function</param>
		/// <param name="e">Contains information on the new GameWindow size.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected void OnResize(object sender, EventArgs e) {
			Game.OnResize(e);
		}

		#endregion

		#region OnUpdateFrame

		/// <summary>
		/// Add your game logic here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected void OnUpdateFrame(object sender, FrameEventArgs e) {
			Game.OnUpdateFrame(e);
		}

		#endregion

		#region OnRenderFrame

		/// <summary>
		/// Add your game rendering code here.
		/// </summary>
		/// <param name="sender">Caller of the function</param>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected void OnRenderFrame(object sender, FrameEventArgs e) {
			Game.OnRenderFrame(e);
		}

		#endregion

		protected void OnMouseMove(object sender, MouseMoveEventArgs e) {
			Game.OnMouseMove(e);
		}
    }
}

