using System;
using ChronosEngine;
using ChronosEngine.Structures;
using ChronosEngine.Shaders;
using OpenTK;

namespace Game {
	class MainClass {
		public static void Main(string[] args) {
			new GameTest().GameEngine.Window.Run(60, 60);
		}
	}
}
