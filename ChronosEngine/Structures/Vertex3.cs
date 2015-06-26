using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Structures {
	public struct Vertex3 {
		public Vector3 Pos;
		public Vector2 TexCoord;

		public Vertex3(Vector3 pos, Vector2 texCoord) {
			this.Pos = pos;
			this.TexCoord = texCoord;
		}

		public Vertex3(Vector3 pos) :
			this(pos, new Vector2(0, 0)) {
		}

		public static int Stride {
			get { return Vector3.SizeInBytes + Vector2.SizeInBytes; }
		}
	}
}
