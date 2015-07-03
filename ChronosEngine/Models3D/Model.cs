using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Interfaces;
using ChronosEngine.Primitives3D;
using ChronosEngine.Shaders;
using ChronosEngine.Structures;
using ChronosEngine.Textures;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Models3D {
	public class Model {
		public Mesh Mesh { get; set; }
		public Texture2D Texture { get; set; }
		public Material Material { get; set; }

		public Vector3 Position { get; set; } = Vector3.Zero;
		public Vector3 Rotation { get; set; } = Vector3.Zero;
		public Vector3 Scale { get; set; } = Vector3.One;

		public Matrix4 RotationMatrix {
			get {
				return Matrix4.CreateRotationX(Rotation.X) *
					Matrix4.CreateRotationY(Rotation.Y) *
					Matrix4.CreateRotationZ(Rotation.Z);
			}
		}

		public Matrix4 ModelMatrix {
			get {
				return Matrix4.CreateTranslation(Position * ChronoGame.UpTransform) *
					this.RotationMatrix * Matrix4.CreateScale(Scale);
			}
		}
		
		public Model(Mesh mesh, Texture2D texture, Material material) {
			this.Mesh = mesh;
			this.Texture = texture;
			this.Material = material;
		}

		public void Bind(Shader shader) {
			shader.Update(ChronoGame.Instance, this);
			Texture.Bind(TextureUnit.Texture0);
			Mesh.Bind();
		}
	}
}
