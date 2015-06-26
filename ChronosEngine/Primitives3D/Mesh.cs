using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Shaders;
using ChronosEngine.Structures;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Primitives3D {
	public class Mesh {
		public int VertexBufferObject { get; set; }
		public int[] VertexArrayBuffers { get; set; }
		public int NumVertices { get; set; }

		public Mesh(Vertex3[] vertices, int numVertices) {
			this.NumVertices = numVertices;
			this.VertexArrayBuffers = new int[(int)Buffers.num_buffers];

			this.VertexBufferObject = GL.GenVertexArray();
			GL.BindVertexArray(VertexBufferObject);

			List<Vector3> pos = new List<Vector3>(numVertices);
			List<Vector2> texcoord = new List<Vector2>(numVertices);

			foreach (Vertex3 v in vertices) {
				pos.Add(v.Pos);
				texcoord.Add(v.TexCoord);
			}

			GL.GenBuffers((int)Buffers.num_buffers, VertexArrayBuffers);

			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArrayBuffers[(int)Buffers.position_buffer]);
			GL.BufferData(BufferTarget.ArrayBuffer, numVertices * Vector3.SizeInBytes, pos.ToArray(), BufferUsageHint.StaticDraw);

			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArrayBuffers[(int)Buffers.texcoord_buffer]);
			GL.BufferData(BufferTarget.ArrayBuffer, numVertices * Vector2.SizeInBytes, texcoord.ToArray(), BufferUsageHint.StaticDraw);

			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

			GL.BindVertexArray(0);
		}

		public void Bind() {
			this.Draw();
		}

		private void Draw() {
			GL.BindVertexArray(VertexBufferObject);
			GL.DrawArrays(PrimitiveType.Triangles, 0, NumVertices);
			GL.BindVertexArray(0);
		}

		internal enum Buffers {
			position_buffer,
			texcoord_buffer,
			num_buffers,
		}
	}
}
