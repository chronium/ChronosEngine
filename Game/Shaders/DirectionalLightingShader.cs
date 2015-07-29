using ChronosEngine;
using ChronosEngine.Camera;
using ChronosEngine.Models3D;
using ChronosEngine.Rendering;
using ChronosEngine.Rendering.Shaders;
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

		public override void UpdateUniforms(Camera camera, Matrix4 modelMatrix) {
			Matrix4 modelViewProj = modelMatrix * camera.ViewProjectionMatrix;
			GL.UniformMatrix4(this.GetUniform("MVP"), false, ref modelViewProj);
			GL.UniformMatrix4(this.GetUniform("model"), false, ref modelMatrix);
            GL.Uniform3(this.GetUniform("eyePos"), camera.Position);
		}
	}
}
