using System;
using ChronosEngine;
using ChronosEngine.Structures;
using ChronosEngine.Shaders;
using OpenTK;
using ChronosEngine.Models3D;
using ChronosEngine.Scene;

namespace Game {
	class MainClass {
		public static void Main(string[] args) {
			new GameTest().Run();
			/*var Scene = new SceneGraph();
			Scene.CreateWorld(new Vector3(0, -9.81f, 0f));
			ColladaLoader.Load("road", Scene);
			Console.Read();*/
		}
	}
}
