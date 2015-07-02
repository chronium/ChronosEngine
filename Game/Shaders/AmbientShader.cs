using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Models3D;
using ChronosEngine.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Game.Shaders {
	class AmbientShader : Shader {
		public AmbientShader() :
			base("Ambient") {
			GL.UseProgram(this.ProgramID);
			GL.Uniform1(this.GetUniform("diffuse"), 0);
			GL.Uniform4(this.GetUniform("ambient"), new Vector4(.25f, .25f, .25f, 1));
			GL.BindAttribLocation(this.ProgramID, 0, "vpos");
			GL.BindAttribLocation(this.ProgramID, 1, "vtexcoord");
			GL.BindAttribLocation(this.ProgramID, 2, "vertcolor");
			GL.BindAttribLocation(this.ProgramID, 3, "vnormal");
		}
		
		public override void Bind(ChronoGame game, Model model) {
			GL.UseProgram(this.ProgramID);
			Matrix4 viewProjection;
			game.Camera.GetViewProjectionMatrix(out viewProjection);
			Matrix4 modelViewProjection = model.ModelMatrix * viewProjection;

			GL.UniformMatrix4(this.GetUniform("mvp"), false, ref modelViewProjection);
		}
	}
}
