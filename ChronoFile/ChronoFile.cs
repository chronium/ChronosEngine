using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoFile {
	public class ChronoFile {
		public string FileName { get; }

		public ChronoFile(string fileName) {
			this.FileName = fileName;
		}
	}
}
