using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Rendering.Scene;
using ChronosEngine.Rendering.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Rendering {
	public class RenderingEngine {
		public Shader BaseShader { get; set; }
		public List<Shader> BlendShaders { get; set; } = new List<Shader>();


		public RenderingEngine(Shader baseShader) {
			this.BaseShader = baseShader;
		}

		public void AddBlendShader(Shader shader) {
			this.BlendShaders.Add(shader);
		}

		private void loadCameraProjection(Camera camera) {
			GL.MatrixMode(MatrixMode.Modelview);
			Matrix4 projection = camera.ProjectionMatrix;
			GL.LoadMatrix(ref projection);
		}

		public void Render(Camera camera, SceneGraph scene) {
			loadCameraProjection(camera);
			this.BaseShader.Bind();
			foreach (Model m in scene.Models)
				m.Bind(this.BaseShader, camera);

			this.Enable3DBlend();
			foreach (Shader shader in this.BlendShaders) {
				shader.Bind();
				foreach (DirectionalLight dirLight in scene.DirectionalLigts) {
					shader.BindDirectionalLight("directionalLight", dirLight);
					foreach (Model m in scene.Models)
						m.Bind(shader, camera);
				}
			}
			this.Disable3DBlend();
		}

		public void Enable3DBlend() {
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
			GL.DepthMask(false);
			GL.DepthFunc(DepthFunction.Equal);
		}
		public void Disable3DBlend() {
			GL.DepthFunc(DepthFunction.Less);
			GL.DepthMask(true);
			GL.Disable(EnableCap.Blend);
		}
	}
}
