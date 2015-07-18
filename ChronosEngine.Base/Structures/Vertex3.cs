using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Base.Structures {
	public struct Vertex3 {
		public Vector3 Pos;
		public Vector2 TexCoord;
		public Vector4 Color;
		public Vector3 Normal;

		public Vertex3(Vector3 pos, Vector2 texCoord, Vector4 color, Vector3 normal) {
			this.Pos = pos;
			this.TexCoord = texCoord * new Vector2(1, -1);
			this.Color = color;
			this.Normal = normal;
		}

		public Vertex3(Vector3 pos, Vector2 texCoord, Vector4 color) :
			this(pos, texCoord, color, new Vector3(1)) {

		}

		public Vertex3(Vector3 pos, Vector2 texCoord) :
			this(pos, texCoord, new Vector4(1)) {

		}

		public Vertex3(Vector3 pos, Vector2 texCoord, Vector3 normal) :
			this(pos, texCoord, new Vector4(1), normal) {

		}

		public Vertex3(Vector3 pos, Vector4 color) :
			this(pos, new Vector2(0, 0), color) {

		}

		public Vertex3(Vector3 pos) :
			this(pos, new Vector2(0, 0)) {
		}

		public static int Stride {
			get { return Vector3.SizeInBytes + Vector2.SizeInBytes + Vector4.SizeInBytes + Vector3.SizeInBytes; }
		}
	}
}
