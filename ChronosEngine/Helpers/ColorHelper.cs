using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace ChronosEngine.Helpers {
	public static class ColorHelper {
		public static Vector4 HexToVector4(int r, int g, int b, int a) {
			return new Vector4(r, g, b, a) / 255;
		}
		public static Color4 HexToColor4(int r, int g, int b, int a) {
			return new Color4((float)r / 255, (float)g / 255, (float)b / 255, (float)a / 255);
		}
	}
}
