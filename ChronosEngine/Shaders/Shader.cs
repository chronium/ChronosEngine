﻿//
//  Shader.cs
//
//  Author:
//       Chronium Silver (Andrei Dimitriu) <onlivechronium@gmail.com>
//
//  Copyright (c) 2015 Chronium @ ChronoStudios
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Text;
using System.Linq;

namespace ChronosEngine.Shaders {
	public abstract class Shader {
		public int ProgramID = -1;
		public int VShaderID = -1;
		public int FShaderID = -1;
		public int AttributeCount = 0;
		public int UniformCount = 0;

		public Dictionary<string, AttributeInfo> Attributes = new Dictionary<string, AttributeInfo>();
		public Dictionary<string, UniformInfo> Uniforms = new Dictionary<string, UniformInfo>();
		public Dictionary<string, uint> Buffers = new Dictionary<string, uint>();

		public Shader() {
			ProgramID = GL.CreateProgram();
		}

		private void loadShader(String code, ShaderType type, out int address) {
			address = GL.CreateShader(type);
			GL.ShaderSource(address, code);
			GL.CompileShader(address);
			GL.AttachShader(ProgramID, address);
			var str = GL.GetShaderInfoLog(address);
			if (str != "")
				Console.WriteLine(str);
		}

		public void LoadShaderFromString(String code, ShaderType type) {
			if (type == ShaderType.VertexShader) {
				loadShader(code, type, out VShaderID);
			}
			else if (type == ShaderType.FragmentShader) {
				loadShader(code, type, out FShaderID);
			}
		}

		public void LoadShaderFromFile(String filename, ShaderType type) {
			using (StreamReader sr = new StreamReader(filename)) {
				if (type == ShaderType.VertexShader) {
					loadShader(sr.ReadToEnd(), type, out VShaderID);
				}
				else if (type == ShaderType.FragmentShader) {
					loadShader(sr.ReadToEnd(), type, out FShaderID);
				}
			}
		}

		public void Link() {
			GL.LinkProgram(ProgramID);

			var str = GL.GetProgramInfoLog(ProgramID);
			if (str != "")
				Console.WriteLine(str);

			GL.GetProgram(ProgramID, GetProgramParameterName.ActiveAttributes, out AttributeCount);
			GL.GetProgram(ProgramID, GetProgramParameterName.ActiveUniforms, out UniformCount);

			for (int i = 0; i < AttributeCount; i++) {
				AttributeInfo info = new AttributeInfo();
				int length = 0;

				StringBuilder name = new StringBuilder();

				GL.GetActiveAttrib(ProgramID, i, 256, out length, out info.size, out info.type, name);

				info.name = name.ToString();
				info.address = GL.GetAttribLocation(ProgramID, info.name);
				Attributes.Add(name.ToString(), info);
				Console.WriteLine(name + " " + info.address);
			}

			for (int i = 0; i < UniformCount; i++) {
				UniformInfo info = new UniformInfo();
				int length = 0;

				StringBuilder name = new StringBuilder();

				GL.GetActiveUniform(ProgramID, i, 256, out length, out info.size, out info.type, name);

				info.name = name.ToString();
				Uniforms.Add(name.ToString(), info);
				info.address = GL.GetUniformLocation(ProgramID, info.name);
				Console.WriteLine(name + " " + info.address);
			}
		}

		public void GenBuffers() {
			for (int i = 0; i < Attributes.Count; i++) {
				uint buffer = 0;
				GL.GenBuffers(1, out buffer);

				Buffers.Add(Attributes.Values.ElementAt(i).name, buffer);
			}

			for (int i = 0; i < Uniforms.Count; i++) {
				uint buffer = 0;
				GL.GenBuffers(1, out buffer);

				Buffers.Add(Uniforms.Values.ElementAt(i).name, buffer);
			}
		}

		public void EnableVertexAttribArrays() {
			for (int i = 0; i < Attributes.Count; i++) {
				GL.EnableVertexAttribArray(Attributes.Values.ElementAt(i).address);
			}
		}

		public void DisableVertexAttribArrays() {
			for (int i = 0; i < Attributes.Count; i++) {
				GL.DisableVertexAttribArray(Attributes.Values.ElementAt(i).address);
			}
		}

		public int GetAttribute(string name) {
			if (Attributes.ContainsKey(name)) {
				return Attributes[name].address;
			}
			else {
				return -1;
			}
		}

		public int GetUniform(string name) {
			if (Uniforms.ContainsKey(name)) {
				return Uniforms[name].address;
			}
			else {
				return -1;
			}
		}

		public uint GetBuffer(string name) {
			if (Buffers.ContainsKey(name)) {
				return Buffers[name];
			}
			else {
				return 0;
			}
		}

		public Shader(String vshader, String fshader, bool fromFile = false) {
			ProgramID = GL.CreateProgram();

			if (fromFile) {
				LoadShaderFromFile(vshader, ShaderType.VertexShader);
				LoadShaderFromFile(fshader, ShaderType.FragmentShader);
			}
			else {
				LoadShaderFromString(vshader, ShaderType.VertexShader);
				LoadShaderFromString(fshader, ShaderType.FragmentShader);
			}

			Link();
			GenBuffers();
		}

		public static T LoadShader<T>(string name) where T: Shader {
			return (T)Activator.CreateInstance(typeof(T), new object[] { String.Format("Assets/Shaders/{0}/{0}.vert", name), String.Format("Assets/Shaders/{0}/{0}.frag", name), true });
		}

		public class UniformInfo {
			public String name = "";
			public int address = -1;
			public int size = 0;
			public ActiveUniformType type;
		}

		public class AttributeInfo {
			public String name = "";
			public int address = -1;
			public int size = 0;
			public ActiveAttribType type;
		}
	}
}

