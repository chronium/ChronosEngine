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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ChronosEngine.Base;
using ChronosEngine.Base.Structures;
using ChronosEngine.Camera;
using ChronosEngine.Interfaces;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace ChronosEngine {
	public class ChronoGame {
		public static Vector3 UpTransform = new Vector3(1, -1, 1);

		/// <summary>
		/// Gets the game engine.
		/// </summary>
		/// <value>The game engine.</value>
		public GameEngine GameEngine { get; }
		
		public static ChronoGame Instance { get; set; }

		public KeyboardDevice Keyboard { get; set; }
		public MouseDevice Mouse { get; set; }

		public ContentManager Content { get; set; }

		public ChronoGame(Resolution res1, Resolution res2, string title) {
			this.GameEngine = new GameEngine(res1, res2, title, this);
			DefaultGlobals.OrthographicProjection = Matrix4.CreateOrthographic(res2.Width, res2.Height, 256f, -256f);

			ChronoGame.Instance = this;
			this.Keyboard = this.GameEngine.Window.Keyboard;
			this.Mouse = this.GameEngine.Window.Mouse;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.ChronoGame"/> class.
		/// </summary>
		/// <param name="title">Window title.</param>
		public ChronoGame(string title = "Untitled")
			: this(new Resolution(800, 600), new Resolution(800, 600), title) {
		}
		
		public ChronoGame(Resolution resolution, string title = "Untitled")
			: this(resolution, resolution, title) {
		}

		public void Run() {
			GameEngine.Window.Run(60, 60);
		}

		/// <summary>
		/// Setup OpenGL and load resources here.
		/// </summary>
		/// <param name="e">Not used.</param>
		public virtual void OnLoad(EventArgs e) {
			this.SetClearColor(Color.Black);
			this.SetViewport(0, 0, GameEngine.GameResolution.Width, GameEngine.GameResolution.Height);
			this.Content = new ContentManager();
			this.Content.LoadPredefinedProviders();
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

		public virtual void OnMouseMove(MouseMoveEventArgs e) { }
		public virtual void OnKeyPress(KeyPressEventArgs e) { }
		public virtual void OnKeyDown(KeyboardKeyEventArgs e) { }
		public virtual void OnKeyUp(KeyboardKeyEventArgs e) { }

		public virtual void OnClosing(CancelEventArgs e) { }

		public void SetClearColor(Color color) {
			GL.ClearColor(color);
		}
		public void SetClearColor(Color4 color) {
			GL.ClearColor(color);
		}
		public void SetClearDepth(float depth) {
			GL.ClearDepth(depth);
		}
		public void SetViewport(int x, int y, int width, int height) {
			GL.Viewport(x, y, width, height);
		}
		public void EnableTextures() {
			this.Enable(EnableCap.Texture2D);
		}
		public void Enable(EnableCap cap) {
			GL.Enable(cap);
		}
		public void SetBlendMode(BlendingFactorSrc src, BlendingFactorDest dest) {
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(src, dest);
		}
		public void CullFaces(CullFaceMode mode) {
			GL.CullFace(mode);
			GL.Enable(EnableCap.CullFace);
		}
		public void Clear() {
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		public void SwapBuffers() {
			GameEngine.Window.SwapBuffers();
		}

		public void Setup3D() {
			this.SetClearDepth(1.0f);
			this.Enable(EnableCap.DepthTest);
			GL.DepthFunc(DepthFunction.Lequal);
			GL.ShadeModel(ShadingModel.Smooth);
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
		}

		/*public void SetupQuaternionCamera3D(float speed, Vector2 mouseSensitivity, CamMode mode) {
			Camera = new QuaternionCamera(GameEngine.Window.Mouse, GameEngine.Window.Keyboard, GameEngine.Window);
			((QuaternionCamera)Camera).SetCameraMode(mode);
			((QuaternionCamera)Camera).MouseXSensitivity = mouseSensitivity.X;
			((QuaternionCamera)Camera).MouseYSensitivity = mouseSensitivity.X;
			((QuaternionCamera)Camera).Speed = speed;
		}*/

		/*public void SetCameraProjectionMatrix() {
			GL.MatrixMode(MatrixMode.Modelview);
			Matrix4 proj;
			Camera.GetViewMatrix(out proj);
			GL.LoadMatrix(ref proj);
		}*/
	}
}

