using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Primitives3D;
using ChronosEngine.Structures;
using OpenTK;

namespace ChronosEngine.Models3D {
	public class ModelLoader : AssetProvider {
		public static Model Load(string modelName) {
			string path = GetAssetPath(modelName);
			string[] lines = File.ReadAllLines(path);

			List<Vector3> positions = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			List<Vector2> texCoords = new List<Vector2>();
			List<Vertex3> vertices = new List<Vertex3>();

			List<uint> indices = new List<uint>();

			foreach (string line in lines) {
				string[] split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				if (split.Length > 0)
					switch (split[0]) {
						case "#":
							continue;
						case "v":
							positions.Add(new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
							break;
						case "vn":
							normals.Add(new Vector3(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
							break;
						case "vt":
							texCoords.Add(new Vector2(float.Parse(split[1]), float.Parse(split[2])));
							break;
						case "f":
							Vertex3 temp1 = ParseVertex(split[1], positions, texCoords, normals);
							Vertex3 temp2 = ParseVertex(split[2], positions, texCoords, normals);
							Vertex3 temp3 = ParseVertex(split[3], positions, texCoords, normals);

							if (!vertices.Contains(temp1))
								vertices.Add(temp1);
							if (!vertices.Contains(temp2))
								vertices.Add(temp2);
							if (!vertices.Contains(temp3))
								vertices.Add(temp3);
							indices.Add((uint)vertices.IndexOf(temp1));
							indices.Add((uint)vertices.IndexOf(temp2));
							indices.Add((uint)vertices.IndexOf(temp3));

							break;
					}
			}

			return new Model(new Mesh(vertices.ToArray(), vertices.Count, indices.ToArray(), indices.Count));
		}

		public static Vertex3 ParseVertex(string vertex, List<Vector3> positions, List<Vector2> texCoords, List<Vector3> normals) {
			string[] vert = vertex.Split('/');
			switch (vert.Length) {
				case 1:
					return new Vertex3(positions[int.Parse(vert[0]) - 1]);
				case 2:
					return new Vertex3(positions[int.Parse(vert[0]) - 1], texCoords[int.Parse(vert[1]) - 1]);
				case 3:
					return new Vertex3(positions[int.Parse(vert[0]) - 1], texCoords[int.Parse(vert[1]) - 1], normals[int.Parse(vert[2]) - 1]);
				default:
					return new Vertex3();
			}
		}

		new public static string GetAssetRoot() {
			return "Assets/Models/";
		}

		new public static string GetAssetPath(string asset) {
			return GetAssetRoot() + asset;
		}
	}
}
