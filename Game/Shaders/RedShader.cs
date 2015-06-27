using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using ChronosEngine.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Game.Shaders {
	public class PassthroughShader: Shader {
		public PassthroughShader() :
			base("Passthrough") {
			GL.UseProgram(this.ProgramID);
			GL.Uniform1(this.GetUniform("diffuse"), 0);
			GL.BindAttribLocation(this.ProgramID, 0, "vpos");
			GL.BindAttribLocation(this.ProgramID, 1, "vtexcoord");
		}

		public override void Update(ChronoGame game) {
			GL.UseProgram(this.ProgramID);
			Matrix4 mv;
			game.Camera.GetModelviewProjectionMatrix(out mv);
			GL.UniformMatrix4(this.GetUniform("mvp"), false, ref mv);
		}
	}
}
