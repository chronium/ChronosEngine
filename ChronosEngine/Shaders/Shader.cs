//
//  Author:
//    Chronium Silver (Andrei Dimitriu) onlivechronium@gmail.com
//
//  Copyright (c) 2015, Chronium @ ChronoStudios
//
//  All rights reserved.
//
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Text;
using System.Linq;
using ChronosEngine.Models3D;
using ChronosEngine.Structures;

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

		public Shader(string name) :
			this(String.Format("Assets/Shaders/{0}/{0}.vert", name), String.Format("Assets/Shaders/{0}/{0}.frag", name), true)
        {
		}

		public void Bind() {
			GL.UseProgram(this.ProgramID);
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

        public abstract void Update(ChronoGame game, Model model);

		public void BindMaterial(string field, Material material) {
			GL.Uniform4(this.GetUniform(field + ".ambient"), material.AmbientColor);
			GL.Uniform1(this.GetUniform(field + ".specularIntensity"), material.SpecularIntensity);
			GL.Uniform1(this.GetUniform(field + ".specularPower"), material.SpecularPower);
		}
	}
}

