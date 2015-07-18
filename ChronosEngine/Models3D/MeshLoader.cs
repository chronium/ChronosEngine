using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Base;
using ChronosEngine.Base.Structures;
using ChronosEngine.Primitives3D;
using OpenTK;

namespace ChronosEngine.Models3D {
	public class MeshLoader : AssetProvider<Mesh> {
		public static Mesh LoadMesh(string modelName) {
			string path = modelName;
			string[] lines = File.ReadAllLines(path);

			List<Vector3> positions = new List<Vector3>();
			List<Vector3> normals = new List<Vector3>();
			List<Vector2> texCoords = new List<Vector2>();
			List<Vertex3> vertices = new List<Vertex3>();

			List<int> indices = new List<int>();

			bool quads = false;

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
							for (int i = 1; i < split.Length; i++) {
								Vertex3 temp = ParseVertex(split[i], positions, texCoords, normals);
								if (!vertices.Contains(temp))
									vertices.Add(temp);
								indices.Add(vertices.IndexOf(temp));
							}
							quads = split.Length > 4;
							break;
					}
			}

			return new Mesh(vertices.ToArray(), vertices.Count, indices.ToArray(), indices.Count, quads);
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

		public MeshLoader(string root) 
			: base(root, "Models/") {

		}

		public override Mesh Load(string assetName, params object[] args) {
			return LoadMesh(assetName);
		}
	}
}
