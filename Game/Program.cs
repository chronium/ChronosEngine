using System;
using ChronosEngine;
using ChronosEngine.Structures;
using ChronosEngine.Shaders;
using OpenTK;

namespace Game {
	class MainClass {
		public static void Main(string[] args) {
			GameEngine game = new GameEngine(new Resolution(800, 600), new Resolution(800, 600), "test");
			game.AddScript("test");
			game.Window.Run(60, 60);
		}
	}
}
