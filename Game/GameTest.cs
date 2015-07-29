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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BulletSharp;
using ChronosEngine;
using ChronosEngine.Base.Structures;
using ChronosEngine.Base.Textures;
using ChronosEngine.Camera;
using ChronosEngine.Models3D;
using ChronosEngine.Rendering;
using ChronosEngine.Rendering.Scene;
using ChronosEngine.Rendering.Shaders;
using Game.Shaders;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game {
	public class GameTest : ChronoGame {
		Texture2D texture;
		Texture2D brick;

		Camera camera;
		RenderingEngine renderingEngine;

		public SceneGraph Scene { get; set; }

		public GameTest() : base() {
		}
		public override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.SetClearColor(new Color4(0.0f, 0.15f, 0.3f, 1.0f));
			this.Setup3D();
			//this.SetupQuaternionCamera3D(4f, new Vector2(.25f), CamMode.NoClip);
			this.CullFaces(CullFaceMode.Back);

			camera = new Camera(60f, this.GameEngine.GameResolution, 0.001f, 256f);
			renderingEngine = new RenderingEngine(new AmbientShader());
			renderingEngine.AddBlendShader(new DirectionalLightingShader());

			texture = Texture2D.LoadTexture("RoadTexture.png");
			brick = Content.Load<Texture2D>("brick1.jpg");

			Scene = new SceneGraph();
			Scene.CreateWorld(new Vector3(0, -9.81f, 0f));

			var light = new DirectionalLight() {
				baseLight = new BaseLight() {
					color = new Vector3(1, 1, 1),
					intensity = 1f,
				},
				direction = new Vector3(1, 1, 1),
			};

			Scene.DirectionalLigts.Add(light);

			var roadMesh = ColladaLoader.Load("road", Scene, Content);
			var ballMesh = Content.Load<Mesh>("ball.obj");
			var roadMaterial = new Material() {
				AmbientColor = new Vector4(0.125f, 0.125f, 0.125f, 1f),
				SpecularIntensity = new Vector4(2f),
				SpecularPower = 64f,
			};

			//var groundBody = Scene.BodyFromShape(new ConvexHullShape(roadMesh[0].Points), Matrix4.CreateTranslation(new Vector3(0, -1, 0)));
			//road = new Model(roadMesh[0], texture, roadMaterial, groundBody);
			Scene.AddModel(new Model(roadMesh[0].model.mesh, roadMesh[0].model.material, Scene.CreateBoxBody(new Vector3(6, 0, 6), Matrix4.CreateTranslation(new Vector3(0, 0, 0)))));

			Matrix4 startMatrix = Matrix4.CreateTranslation(0, 4, 0);
			Scene.AddCharacter(startMatrix, 0.5f, 1.75f);
		}

		Vector3 Movement;
		float Speed = 1.388f;

		public override void OnClosing(CancelEventArgs e) {
			Scene.CharacterController.Jump();
		}

		protected void UpdateMovement(float dt) {
			if (Keyboard[Key.W] && !Keyboard[Key.S]) {
				Movement.Z = 0;
				Movement.Z -= Speed * dt;
			} else if (Keyboard[Key.S] && !Keyboard[Key.W]) {
				Movement.Z = 0;
				Movement.Z += Speed * dt;
			} else Movement.Z = 0.0f;

			if (Keyboard[Key.A] && !Keyboard[Key.D]) {
				Movement.X = 0.0f;
				Movement.X -= Speed * dt;
			} else if (Keyboard[Key.D] && !Keyboard[Key.A]) {
				Movement.X = 0.0f;
				Movement.X += Speed * dt;
			} else
				Movement.X = 0.0f;
		}

	public override void OnUpdateFrame(FrameEventArgs e) {
			GameEngine.Window.Title = "FPS: " +  Fps.GetFps(e.Time).ToString();

			//Camera.UpdateMouse(e.Time);
			UpdateMovement((float)e.Time);

			//Vector3 mov = Vector3.Transform(Movement, ((QuaternionCamera)Camera).TargetOrientationY.Inverted());
			//Scene.CharacterController.SetWalkDirection(mov);

			if (Scene.CharacterController.OnGround)
				if (Keyboard[Key.Space])
					Scene.CharacterController.Jump();

			if (Keyboard[Key.ShiftLeft] || Keyboard[Key.ShiftRight])
				Speed = 12.4f;
			else
				Speed = 1.388f;

			Matrix4 mat;
			Scene.CharacterController.GhostObject.GetWorldTransform(out mat);
			//((QuaternionCamera)Camera).Position = mat.ExtractTranslation();

			Scene.World.world.StepSimulation((float)e.Time, 60, 1 / 60.0f);
		}
		
		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();

			renderingEngine.Render(camera, Scene);

			this.SwapBuffers();
		}
	}
}
