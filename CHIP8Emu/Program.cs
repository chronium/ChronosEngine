﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace CHIP8Emu {
	class Program {
		static void Main(string[] args) {
			new CHIP8(args[0], clockSpeed: 256).Run();
        }
	}
}
