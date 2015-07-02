using ChronosEngine;
using ChronosEngine.Models3D;
using ChronosEngine.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Game.Shaders {
	public class DirectionalLightingShader: Shader {
		public DirectionalLightingShader() :
			base("Directional") {
			GL.UseProgram(this.ProgramID);
			GL.Uniform1(this.GetUniform("diffuse"), 0);
			GL.BindAttribLocation(this.ProgramID, 0, "vpos");
			GL.BindAttribLocation(this.ProgramID, 1, "vtexcoord");
            GL.BindAttribLocation(this.ProgramID, 2, "vertcolor");
			GL.BindAttribLocation(this.ProgramID, 3, "vnormal");
		}

		public override void Bind(ChronoGame game, Model model) {
			GL.UseProgram(this.ProgramID);
			Matrix4 viewProjection;
			game.Camera.GetViewProjectionMatrix(out viewProjection);
			Matrix4 modelViewProj = model.ModelMatrix * viewProjection;
			GL.UniformMatrix4(this.GetUniform("mvp"), false, ref modelViewProj);

			Matrix4 view;
			game.Camera.GetViewMatrix(out view);
			Matrix4 modelView = model.ModelMatrix * view;

			GL.UniformMatrix4(this.GetUniform("mv"), false, ref modelView);
		}
	}
}
