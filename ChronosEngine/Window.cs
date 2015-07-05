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
using ChronosEngine.Structures;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

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

		new public event EventHandler<MouseMoveEventArgs> MouseMove;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.Window"/> class.
		/// </summary>
		/// <param name="screenResolution">Screen resolution.</param>
		/// <param name="gameResolution">Game resolution.</param>
		/// <param name="title">Title.</param>
		public Window(Resolution screenResolution, Resolution gameResolution, string title)
			: base(screenResolution.Width, screenResolution.Height, new GraphicsMode(new ColorFormat(8, 8, 8, 8), 8, 16, 4, new ColorFormat(8, 8, 8, 8), 2, false), title) {
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

		protected override void OnMouseMove(MouseMoveEventArgs e) {
			if (MouseMove != null)
				MouseMove.Invoke(this, e);
		}
	}
}

