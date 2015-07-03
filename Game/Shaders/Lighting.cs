using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Game.Shaders {
	public struct BaseLight {
		public Vector3 color;
		public float intensity;
	}

	public struct DirectionalLight {
		public BaseLight baseLight;
		public Vector3 direction;
	}
}
