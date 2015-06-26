using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronosEngine.FileStructure {
	public enum NodeType : byte {
		Byte = 0,
		Short = 1,
		Int = 2,
		Long = 3,
		String = 4,
		Bool = 5,
		Node = 6,
		List = 7
	}
}
