using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assimp;
using Assimp.Configs;
using BulletSharp;
using ChronosEngine.Scene;
using ChronosEngine.Structures;
using ChronosEngine.Textures;
using OpenTK;

namespace ChronosEngine.Models3D {
	public class ColladaLoader : AssetProvider {
		public static List<ModelStr> Load(string name, SceneGraph graph) {
			var materialList = new List<Structures.Material>();

			var modelList = new List<ModelStr>();
			var modelDict = new Dictionary<string, ColladaModel>();

			if (File.Exists(GetAssetPath(name))) {
				Console.WriteLine("---Loading asset---");
				Console.WriteLine("Asset type:   Collada model (dae)");
				Console.WriteLine("Asset name:   " + name);

				AssimpContext importer = new AssimpContext();
				NormalSmoothingAngleConfig config = new NormalSmoothingAngleConfig(66.0f);
				importer.SetConfig(config);

				LogStream logstream = new LogStream(delegate (string msg, string userData) {
					Console.Write(msg);
				});
				logstream.Attach();

				Assimp.Scene scene = importer.ImportFile(GetAssetPath(name), PostProcessPreset.TargetRealTimeMaximumQuality);

				if (scene.HasMaterials)
					foreach (Assimp.Material mat in scene.Materials) {
						materialList.Add(new Structures.Material() {
							AmbientColor = new Vector4(0.125f, 0.125f, 0.125f, 1f),
							SpecularIntensity = new Vector4(mat.ColorSpecular.R, mat.ColorSpecular.G, mat.ColorSpecular.B, mat.ColorSpecular.A),
							SpecularPower = 64f,
							TextureDiffuse = Texture2D.LoadTexture(mat.TextureDiffuse.FilePath)
						});
					}

				foreach (Mesh mesh in scene.Meshes) {
					var meshLoaded = LoadMesh(mesh);
					var material = materialList[mesh.MaterialIndex];
					modelDict.Add(mesh.Name, new ColladaModel() {
						mesh = meshLoaded,
						material = material,
					});
				}

				var tr = scene.RootNode.Transform;
				Matrix4 mato = new Matrix4(tr.A1, tr.A2, tr.A3, tr.A4, tr.B1, tr.B2, tr.B3, tr.B4, tr.C1, tr.C2, tr.C3, tr.C4, tr.D1, tr.D2, tr.D3, tr.D4);

				foreach (var v in scene.RootNode.Children) {
					var t = v.Transform;
					Matrix4 mat = new Matrix4(t.A1, t.A2, t.A3, t.A4, t.B1, t.B2, t.B3, t.B4, t.C1, t.C2, t.C3, t.C4, t.D1, t.D2, t.D3, t.D4);
					mat *= mato;
					var mod = modelDict.ElementAt(v.MeshIndices[0]).Value;
					ModelStr str = new ModelStr() {
						model = mod,
						transform = mat,
					};
					modelList.Add(str);
				}

				importer.Dispose();
			}
			return modelList;
		}

		public static Primitives3D.Mesh LoadMesh(Mesh mesh) {
			List<Vector3> positions = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			List<Vector2> texCoords = new List<Vector2>();
			List<Vertex3> vertices = new List<Vertex3>();

			foreach (Vector3D vect in mesh.Vertices)
				positions.Add(new Vector3(vect.X, vect.Z, -vect.Y));
			foreach (Vector3D norm in mesh.Normals)
				normals.Add(new Vector3(norm.X, norm.Z, -norm.Y));
			foreach (Vector3D coord in mesh.TextureCoordinateChannels[0])
				texCoords.Add(new Vector2(coord.X, coord.Y));

			for (int i = 0; i < mesh.Vertices.Count; i++)
				vertices.Add(new Vertex3(positions[i], texCoords[i], normals[i]));

			return new Primitives3D.Mesh(vertices.ToArray(), vertices.Count, mesh.GetIndices(), mesh.GetIndices().Length, false);
		}

		new public static string AssetRoot {
			get {
				return "Assets/Models/";
			}
		}

		new public static string GetAssetPath(string asset) {
			return string.Format("{0}{1}/{1}.dae", AssetRoot, asset);
		}
	}

	public struct ColladaModel {
		public Primitives3D.Mesh mesh;
		public Structures.Material material;
	}

	public struct ModelStr {
		public ColladaModel model;
		public Matrix4 transform;
	}
}
