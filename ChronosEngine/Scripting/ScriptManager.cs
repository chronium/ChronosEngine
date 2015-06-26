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
using System.IO;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;

namespace ChronosEngine.Scripting {
	public class ScriptManager {
		/// <summary>
		/// Gets the scripts.
		/// </summary>
		/// <value>The scripts.</value>
		public List<Script> Scripts { get; }
		/// <summary>
		/// Gets the scripting engine.
		/// </summary>
		/// <value>The scripting engine.</value>
		public ScriptEngine ScriptingEngine { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.Scripting.ScriptManager"/> class.
		/// </summary>
		public ScriptManager() {
			this.Scripts = new List<Script>();
			ScriptingEngine = Python.CreateEngine();
		}

		/// <summary>
		/// Adds a new script.
		/// </summary>
		/// <param name="script">Script name.</param>
		public void AddScript(string scriptName) {
			try {
				string scriptSource = File.ReadAllText("Assets/Scripts/" + scriptName + ".py");
				Scripts.Add(new Script(scriptSource, ScriptingEngine));
			} catch (FileNotFoundException e) {
				Console.WriteLine("Script \"{0}\" could not be found", scriptName);
				Console.WriteLine(e);
			}
		}

		public void LoadScripts() {
			foreach (Script s in Scripts) {
				dynamic loadFunc;
				if (s.ScriptScope.TryGetVariable("load", out loadFunc))
					loadFunc();
			}
		}
	}
}

