using ChronosEngine;
using ChronosEngine.Camera;
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

		public override void Update(ChronoGame game, Model model) {
			Matrix4 viewProjection;
			game.Camera.GetViewProjectionMatrix(out viewProjection);
			Matrix4 modelViewProj = model.ModelMatrix * viewProjection;
			GL.UniformMatrix4(this.GetUniform("MVP"), false, ref modelViewProj);
		
			Matrix4 modelView = model.ModelMatrix;
			GL.UniformMatrix4(this.GetUniform("model"), false, ref modelView);

			this.BindMaterial("material", model.Material);

			var camPos = ((QuaternionCamera)game.Camera).Position;
            GL.Uniform3(this.GetUniform("eyePos"), camPos);
		}

		public void BindDirectionalLight(string name, DirectionalLight light) {
			this.BindBaseLight(name + ".base", light.baseLight);
			GL.Uniform3(this.GetUniform(name + ".direction"), light.direction);
		}

		public void BindBaseLight(string name, BaseLight light) {
			GL.Uniform3(this.GetUniform(name + ".color"), light.color);
			GL.Uniform1(this.GetUniform(name + ".intensity"), light.intensity);
		}
	}
}
