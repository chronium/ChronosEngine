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
using ChronosEngine.Base.Structures;
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
	}
}

