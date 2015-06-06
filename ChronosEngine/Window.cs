//
//  Window.cs
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
using ChronosEngine.Structures;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine {
	/// <summary>
	/// Window as container for the OpenGL rendering.
	/// </summary>
	public class Window : GameWindow {
		/// <summary>
		/// Gets the screen resolution.
		/// </summary>
		/// <value>The screen resolution.</value>
		public Resolution ScreenResolution { get; private set; }

		/// <summary>
		/// Gets the game resolution.
		/// </summary>
		/// <value>The game resolution.</value>
		public Resolution GameResolution { get; private set; }

		/// <summary>
		/// Occurs before the window is displayed for the first time.
		/// </summary>
		new public event EventHandler<EventArgs> Load;
		/// <summary>
		/// Occurs whenever the window is resized.
		/// </summary>
		new public event EventHandler<EventArgs> Resize;
		/// <summary>
		/// Occurs when it is time to render a frame.
		/// </summary>
		new public event EventHandler<FrameEventArgs> RenderFrame;
		/// <summary>
		/// Occurs when it is time to update a frame.
		/// </summary>
		new public event EventHandler<FrameEventArgs> UpdateFrame;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.Window"/> class.
		/// </summary>
		/// <param name="screenResolution">Screen resolution.</param>
		/// <param name="gameResolution">Game resolution.</param>
		/// <param name="title">Title.</param>
		public Window(Resolution screenResolution, Resolution gameResolution, string title)
			: base(screenResolution.Width, screenResolution.Height, GraphicsMode.Default, title) {
			this.ScreenResolution = screenResolution;
			this.GameResolution = gameResolution;
		}

		/// <summary>
		/// Called after an OpenGL context has been established, but before entering the main loop.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e) {
			if (Load != null)
				Load.Invoke(this, e);
		}

		/// <summary>
		/// Called when the NativeWindow is resized.
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize(EventArgs e) {
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.Viewport(0, 0, ScreenResolution.Width, ScreenResolution.Height);

			if (Resize != null)
				Resize.Invoke(this, e);
		}

		/// <summary>
		/// Called when the frame is rendered.
		/// </summary>
		/// <param name="e">Contains information necessary for frame rendering.</param>
		protected override void OnRenderFrame(FrameEventArgs e) {
			if (RenderFrame != null)
				RenderFrame.Invoke(this, e);
		}

		/// <summary>
		/// Called when the frame is updated.
		/// </summary>
		/// <param name="e">Contains information necessary for frame updating.</param>
		protected override void OnUpdateFrame(FrameEventArgs e) {
			if (UpdateFrame != null)
				UpdateFrame.Invoke(this, e);
		}
	}
}

