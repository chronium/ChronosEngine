using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosEngine {

	public static class Fps {
		static double time = 0.0, frames = 0.0;
		static double fps = 0;

		public static double GetFps(double time) {
			Fps.time += time;
			if (Fps.time < 1.0) {
				frames++;
				return fps;
			} else {
				fps = (int)frames;
				Fps.time = 0.0;
				frames = 0.0;
				return fps;
			}
		}
	}
}
