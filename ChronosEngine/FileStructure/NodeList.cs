using System.Collections.Generic;
using System.Text;

namespace ChronosEngine.FileStructure {
	public class NodeList {
		public List<Node> Nodes { get; set; }

		public NodeList(List<Node> nodes) {
			this.Nodes = nodes;
		}

		public Node this[int node] {
			get {
				return Nodes[node];
			}
			set {
				if (node == -1)
					Nodes.Add(value);
				Nodes[node] = value;
			}
		}
		
		public Node this[string name] {
			get {
				foreach (Node node in Nodes)
					if (node.NodeName == name)
						return node;
				return null;
			}
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append("{" + Nodes[0].ToString());
			for (int i = 1; i < Nodes.Count; i++)
				sb.Append(" ," + Nodes[i]);
			sb.Append("}");
			return sb.ToString();
		}
	}
}
