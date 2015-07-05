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
using BulletSharp;
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
		Texture2D texture;
		Texture2D brick;

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
			brick = Texture2D.LoadTexture("brick1.jpg");

			var roadMesh = ModelLoader.LoadMesh("road.obj");
			var ballMesh = ModelLoader.LoadMesh("ball.obj");
			var roadMaterial = new Material() {
				AmbientColor = new Vector4(0.125f, 0.125f, 0.125f, 1f),
				SpecularIntensity = 2f,
				SpecularPower = 64f,
			};

			light = new DirectionalLight() {
				baseLight = new BaseLight() {
					color = new Vector3(1, 1, 1),
					intensity = 1f,
				},
				direction = new Vector3(1, 1, 1),
			};

			BroadphaseInterface broadphase = new DbvtBroadphase();
			DefaultCollisionConfiguration config = new DefaultCollisionConfiguration();
			CollisionDispatcher dispatcher = new CollisionDispatcher(config);
			SequentialImpulseConstraintSolver solver = new SequentialImpulseConstraintSolver();
			world = new DiscreteDynamicsWorld(dispatcher, broadphase, solver, config);
			world.Gravity = new Vector3(0, -9.81f, 0);
			world.DispatchInfo.AllowedCcdPenetration = 0.0001f;

			CollisionShape groundShape = new BoxShape(6, 0, 6);
			DefaultMotionState groundMotionState = new DefaultMotionState(Matrix4.CreateTranslation(new Vector3(0, -1, 0)));
			RigidBodyConstructionInfo groundRigidBodyCI = new RigidBodyConstructionInfo(0, groundMotionState, groundShape, new Vector3(0));
			var groundRigidBody = new RigidBody(groundRigidBodyCI);
			world.AddRigidBody(groundRigidBody);

			road = new Model(roadMesh, texture, roadMaterial, groundRigidBody);

			Matrix4 startMatrix = Matrix4.CreateTranslation(0, 4, 0);
			ghostObject = new PairCachingGhostObject();
			ghostObject.WorldTransform = startMatrix;
			broadphase.OverlappingPairCache.SetInternalGhostPairCallback(new GhostPairCallback());

			ConvexShape capsule = new CapsuleShape(0.75f, 1.75f);
			ghostObject.CollisionShape = capsule;
			ghostObject.CollisionFlags = CollisionFlags.CharacterObject;

			character = new KinematicCharacterController(ghostObject, capsule, .35f);
			world.AddCollisionObject(ghostObject, CollisionFilterGroups.CharacterFilter, CollisionFilterGroups.StaticFilter | CollisionFilterGroups.DefaultFilter);
			world.AddAction(character);
		}

		PairCachingGhostObject ghostObject;
		KinematicCharacterController character;
        DiscreteDynamicsWorld world;

		Vector3 Movement;
		float Speed = 4f;

		protected void UpdateMovement() {
			Speed /= 60f;

			if (Keyboard[Key.W] && !Keyboard[Key.S]) {
				Movement.Z = 0;
				Movement.Z -= Speed;
			} else if (Keyboard[Key.S] && !Keyboard[Key.W]) {
				Movement.Z = 0;
				Movement.Z += Speed;
			} else Movement.Z = 0.0f;

			if (Keyboard[Key.A] && !Keyboard[Key.D]) {
				Movement.X = 0.0f;
				Movement.X -= Speed;
			} else if (Keyboard[Key.D] && !Keyboard[Key.A]) {
				Movement.X = 0.0f;
				Movement.X += Speed;
			} else
				Movement.X = 0.0f;
		}

	public override void OnUpdateFrame(FrameEventArgs e) {
			GameEngine.Window.Title = "FPS: " +  Fps.GetFps(e.Time).ToString();

			Camera.UpdateMouse(e.Time);
			UpdateMovement();

			Vector3 mov = Vector3.Transform(Movement, ((QuaternionCamera)Camera).TargetOrientationY.Inverted());
			character.SetWalkDirection(mov);

			if (character.OnGround)
				if (Keyboard[Key.Space])
					character.Jump();

			if (Keyboard[Key.ShiftLeft] || Keyboard[Key.ShiftRight])
				Speed = 6f;
			else
				Speed = 4f;

			Matrix4 mat;
			character.GhostObject.GetWorldTransform(out mat);
			((QuaternionCamera)Camera).Position = mat.ExtractTranslation();

			world.StepSimulation((float)e.Time, 60, 1 / 60.0f);
		}
		
		public override void OnRenderFrame(FrameEventArgs e) {
			this.Clear();
			this.SetCameraProjectionMatrix();

			ambientShader.Bind();
			road.Bind(ambientShader);

			this.Enable3DBlend();
			
			tempLightShader.Bind();
			((DirectionalLightingShader)tempLightShader).BindDirectionalLight("directionalLight", light);
			road.Bind(tempLightShader);

			this.Disable3DBlend();

			this.SwapBuffers();
		}
	}
}
