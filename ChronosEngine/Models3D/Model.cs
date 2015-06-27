using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Primitives3D;
using OpenTK;

namespace ChronosEngine.Models3D {
	public class Model {
		public Mesh Mesh { get; set; }

		public Model(Mesh mesh) {
			this.Mesh = mesh;
		}
	}
}
