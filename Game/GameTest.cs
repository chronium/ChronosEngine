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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ChronosEngine;
using ChronosEngine.Camera;
using ChronosEngine.Models3D;
using ChronosEngine.Primitives3D;
using ChronosEngine.Shaders;
using ChronosEngine.Structures;
using ChronosEngine.Textures;
using Game.Shaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game {
	public class GameTest : ChronoGame {
		Shader ambientShader;
		Shader tempLightShader;
		Model road;
		Model road1;
		Texture2D texture;

		DirectionalLight light;

		public GameTest() : base() {
		}
		public override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.SetClearColor(new Color4(0.0f, 0.15f, 0.3f, 1.0f));
			this.Setup3D();
			this.SetupQuaternionCamera3D(4f, new Vector2(.25f), CamMode.NoClip);
			this.CullFaces(CullFaceMode.Back);

			ambientShader = new AmbientShader();
			tempLightShader = new DirectionalLightingShader();

			texture = Texture2D.LoadTexture("RoadTexture.png");

			var roadMesh = ModelLoader.LoadMesh("road.obj");
			var roadMaterial = new Material() {
				AmbientColor = new Vector4(0.125f, 0.125f, 0.125f, 1f),
				SpecularIntensity = 2f,
				SpecularPower = 64f,
			};

			road = new Model(roadMesh, texture, roadMaterial);
			road1 = new Model(roadMesh, texture, roadMaterial);

			road1.Position = new Vector3(2, 0, 0);

			light = new DirectionalLight() {
				baseLight = new BaseLight() {
					color = new Vector3(1f, 1, 1),
					intensity = 1f,
				},
				direction = new Vector3(1, 1, 1),
			};
		}

		public override void OnUpdateFrame(FrameEventArgs e) {
			GameEngine.Window.Title = "FPS: " +  Fps.GetFps(e.Time).ToString();

			Camera.Update(e.Time);
		}
		
		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();
			this.SetCameraProjectionMatrix();

			ambientShader.Bind();
			road.Bind(ambientShader);
			road1.Bind(ambientShader);

			this.Enable3DBlend();

			tempLightShader.Bind();
			((DirectionalLightingShader)tempLightShader).BindDirectionalLight("directionalLight", light);
			road.Bind(tempLightShader);
            road1.Bind(tempLightShader);

			this.Disable3DBlend();

			this.SwapBuffers();
		}
	}
}
