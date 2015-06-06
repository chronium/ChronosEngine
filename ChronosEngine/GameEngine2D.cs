//
//  GameEngine2D.cs
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
using ChronosEngine.Structures;
using System.Collections.Generic;
using ChronosEngine.Shaders;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using ChronosEngine.Textures;
using ChronosEngine.Render2D;

namespace ChronosEngine {
	public class GameEngine2D {
		private Matrix4 Orthographic;

		private Resolution GameResolution { get; set; }

		public GameEngine2D(Resolution gameResolution) {
			this.GameResolution = gameResolution;

			Orthographic = Matrix4.CreateOrthographic(GameResolution.Width, GameResolution.Height, 64f, -64f);
		}

		public void Load(EventArgs e) {
			GL.ClearColor(Color.CornflowerBlue);
			GL.Viewport(0, 0, GameResolution.Width, GameResolution.Height);

			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		public void Resize(EventArgs e) {
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			//GL.Ortho(0, Window.GameResolution.Width, Window.GameResolution.Height, 0, 0.0, 4.0);
		}

		public void Update(FrameEventArgs e) {
		}

		public void Render(FrameEventArgs e) {
			GL.PushMatrix();
			GL.LoadMatrix(ref Orthographic);

			GL.PopMatrix();
		}
	}
}

