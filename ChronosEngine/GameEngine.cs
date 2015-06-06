//
//  Engine.cs
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
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using OpenTK.Graphics;
using ChronosEngine.Structures;
using ChronosEngine.Render2D.DebugPrimitives;
using ChronosEngine.Shaders;

namespace ChronosEngine {
	public class GameEngine {
		/// <summary>
		/// The instance.
		/// </summary>
		public static GameEngine Instance;

		/// <summary>
		/// Gets the window.
		/// </summary>
		/// <value>The window.</value>
		public Window Window { get; private set; }

		/// <summary>
		/// The current view.
		/// </summary>
		public RectangleF CurrentView = new RectangleF(0, 0, 800, 600);

		/// <summary>
		/// The engine.
		/// </summary>
		public GameEngine2D engine;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.GameEngine"/> class.
		/// </summary>
		/// <param name="screenResolution">Screen resolution.</param>
		/// <param name="gameResolution">Game resolution.</param>
		/// <param name="windowTitle">Window title.</param>
		public GameEngine(Resolution screenResolution, Resolution gameResolution, string windowTitle) {
			Instance = this;

			Window = new Window(screenResolution, gameResolution, windowTitle);
			Window.KeyDown += Keyboard_KeyDown;
			Window.Load += OnLoad;
			Window.Resize += OnResize;
			Window.RenderFrame += OnRenderFrame;
			Window.UpdateFrame += OnUpdateFrame;
		
			engine = new GameEngine2D(gameResolution);
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

		/// <summary>
		/// Setup OpenGL and load resources here.
		/// </summary>
		/// <param name="sender">Caller of the function</param>
		/// <param name="e">Not used.</param>
		public void OnLoad(object sender, EventArgs e) {
			GL.ClearColor(Color.MidnightBlue);
			GL.Enable(EnableCap.LineSmooth);
			GL.Enable(EnableCap.Multisample);
			GL.Enable(EnableCap.PolygonSmooth);
			GL.Enable(EnableCap.Texture2D);

			engine.Load(e);
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
			engine.Resize(e);
		}

		#endregion

		#region OnUpdateFrame

		/// <summary>
		/// Add your game logic here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		/// <remarks>There is no need to call the base implementation.</remarks>
		protected void OnUpdateFrame(object sender, FrameEventArgs e) {
			engine.Update(e);
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
			GL.Clear(ClearBufferMask.ColorBufferBit);

			engine.Render(e);

			Window.SwapBuffers();
		}

		#endregion
	}
}

