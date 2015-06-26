using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine.Interfaces;

namespace ChronosEngine.FileStructure {
	public class ChronoFile {
		public Node RootNode { get; set; } = new Node("Root", NodeType.List, new NodeList(new List<Node>()));
		public string FileName { get; }

		public ChronoFile(string fileName) {
			this.FileName = fileName;
		}

		public void AddNode(Node node) {
			var nodeList = (NodeList) RootNode.NodeValue;
			nodeList.Nodes.Add(node);
		}

		public void Save() {
			Encode(this);
		}

		public static void Encode(ChronoFile file) {
			try {
				using (FileStream fileStream = new FileStream(file.FileName, FileMode.CreateNew)) {
					Node.Encode(file.RootNode, new BinaryWriter(fileStream));
					fileStream.Flush();
				}
			} catch (IOException) {
				File.Delete(file.FileName);
				Encode(file);
            }
		}

		public static ChronoFile Decode(string fileName) {
			if (File.Exists(fileName)) {
				using (FileStream fileStream = new FileStream(fileName, FileMode.Open)) {
					var root = Node.Decode(new BinaryReader(fileStream));
					var file = new ChronoFile(fileName);
					file.RootNode = root;
                    return file;
				}
			}

			return null;
		}

		public override string ToString() {
			return string.Format("{0}: {1}", Path.GetFileName(this.FileName), this.RootNode);
		}
	}
}
