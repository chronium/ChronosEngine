using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Base.Textures;
using OpenTK;

namespace ChronosEngine.Base.Structures {
	public struct Material {
		public Vector4 AmbientColor;
		public Vector4 SpecularIntensity;
		public float SpecularPower;
		public Texture2D TextureDiffuse;
	}
}
