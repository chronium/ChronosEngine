using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Models3D;
using ChronosEngine.Rendering;
using ChronosEngine.Rendering.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Game.Shaders {
	class AmbientShader : Shader {
		public AmbientShader() :
			base("Ambient") {
			GL.UseProgram(this.ProgramID);
			GL.Uniform1(this.GetUniform("diffuse"), 0);
			GL.BindAttribLocation(this.ProgramID, 0, "vpos");
			GL.BindAttribLocation(this.ProgramID, 1, "vtexcoord");
			GL.BindAttribLocation(this.ProgramID, 2, "vertcolor");
			GL.BindAttribLocation(this.ProgramID, 3, "vnormal");
		}
		
		public override void UpdateUniforms(Camera camera, Matrix4 modelMatrix) {
			Matrix4 modelViewProjection = modelMatrix * camera.ViewProjectionMatrix;
			GL.UniformMatrix4(this.GetUniform("mvp"), false, ref modelViewProjection);
		}
	}
}
