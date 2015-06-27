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
			GL.Uniform4(this.GetUniform("ambient"), new Vector4(.25f, .25f, .25f, 1));
			GL.BindAttribLocation(this.ProgramID, 0, "vpos");
			GL.BindAttribLocation(this.ProgramID, 1, "vtexcoord");
            GL.BindAttribLocation(this.ProgramID, 2, "vertcolor");
			GL.BindAttribLocation(this.ProgramID, 3, "vnormal");
		}

		public override void Update(ChronoGame game) {
			GL.UseProgram(this.ProgramID);
			Matrix4 mv;
			game.Camera.GetModelviewProjectionMatrix(out mv);
			GL.UniformMatrix4(this.GetUniform("mvp"), false, ref mv);
			game.Camera.GetProjectionMatrix(out mv);
			GL.UniformMatrix4(this.GetUniform("mv"), false, ref mv);
		}
	}
}
