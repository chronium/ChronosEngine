using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;
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
		public RigidBody RigidBody { get; set; }
		public Vector3 Scale { get; set; } = Vector3.One;

		public Matrix4 ModelMatrix {
			get {
				Matrix4 temp;
				RigidBody.GetWorldTransform(out temp);
				return temp * Matrix4.CreateScale(Scale);
			}
		}
		
		public Model(Mesh mesh, Texture2D texture, Material material, RigidBody rigidBody) {
			this.Mesh = mesh;
			this.Texture = texture;
			this.Material = material;
			this.RigidBody = rigidBody;
		}

		public void Bind(Shader shader) {
			shader.Update(ChronoGame.Instance, this);
			Texture.Bind(TextureUnit.Texture0);
			Mesh.Bind();
		}
	}
}
