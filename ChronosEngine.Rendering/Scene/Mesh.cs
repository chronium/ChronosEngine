﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Base;
using ChronosEngine.Base.Structures;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ChronosEngine.Rendering.Scene {
	public class Mesh : Asset {
		public int VertexBufferObject { get; set; }
		public int[] VertexArrayBuffers { get; set; }
		public int DrawCount { get; set; }

		public bool Quad { get; set; }

		public List<Vector3> Points { get; set; }

		public Mesh(Vertex3[] vertices, int numVertices, int[] inidices, int numIndices, bool quad) {
			this.DrawCount = numIndices;
			this.VertexArrayBuffers = new int[(int)Buffers.num_buffers];
			this.Quad = quad;

			this.VertexBufferObject = GL.GenVertexArray();
			GL.BindVertexArray(VertexBufferObject);

			List<Vector3> pos = new List<Vector3>(numVertices);
			List<Vector2> texcoord = new List<Vector2>(numVertices);

			foreach (Vertex3 v in vertices) {
				pos.Add(v.Pos);
				texcoord.Add(v.TexCoord);
			}

			this.Points = pos;

			GL.GenBuffers((int)Buffers.num_buffers, VertexArrayBuffers);

			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArrayBuffers[(int)Buffers.vertex_buffer]);
			GL.BufferData(BufferTarget.ArrayBuffer, numVertices * Vertex3.Stride, vertices, BufferUsageHint.StaticDraw);

			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex3.Stride, 0);
			GL.EnableVertexAttribArray(1);
			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vertex3.Stride, Vector3.SizeInBytes);
			GL.EnableVertexAttribArray(2);
			GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Vertex3.Stride, Vector3.SizeInBytes + Vector2.SizeInBytes);
			GL.EnableVertexAttribArray(3);
			GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, Vertex3.Stride, Vector3.SizeInBytes + Vector2.SizeInBytes + Vector4.SizeInBytes);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, VertexArrayBuffers[(int)Buffers.index_buffer]);
			GL.BufferData(BufferTarget.ElementArrayBuffer, numIndices * sizeof(int), inidices.ToArray(), BufferUsageHint.StaticDraw);

			GL.BindVertexArray(0);
		}

		public void Bind() {
			this.Draw();
		}

		private void Draw() {
			GL.BindVertexArray(VertexBufferObject);
			GL.DrawElements(!Quad ? PrimitiveType.Triangles : PrimitiveType.Quads, DrawCount, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0);
		}

		internal enum Buffers {
			vertex_buffer,
			index_buffer,
			num_buffers,
		}
	}
}
