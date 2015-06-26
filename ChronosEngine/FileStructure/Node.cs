using System;
using System.Collections.Generic;
using System.IO;
using ChronosEngine.Helpers;
using ChronosEngine.Interfaces;

namespace ChronosEngine.FileStructure {
	public class Node : IEquatable<Node> {
		public string NodeName;
		public NodeType NodeType;
		public object NodeValue;

		public Node(string nodeName, NodeType nodeType, object nodeValue) {
			this.NodeName = nodeName;
			this.NodeType = nodeType;
			this.NodeValue = nodeValue;
		}

		public override string ToString() {
			string type = Enum.GetName(typeof(NodeType), NodeType);
			return string.Format("(\"{0}\",{1}):{2}", NodeName, type, NodeValue.ToString());
		}
		
		public override bool Equals(object obj) {
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;

			return obj.GetType() == GetType() && Equals((Node)obj);
		}

		public bool Equals(Node other) {
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;

			return NodeName.Equals(other.NodeName)
				   && NodeType.Equals(other.NodeType)
				   && NodeValue.Equals(other.NodeValue);
		}

		public static bool operator ==(Node obj1, Node obj2) {
			if (ReferenceEquals(obj1, null))
				return false;
			if (ReferenceEquals(obj2, null))
				return false;

			return obj1.NodeName.Equals(obj2.NodeName)
				   && obj1.NodeType.Equals(obj2.NodeType)
				   && obj1.NodeValue.Equals(obj2.NodeValue);
		}

		public static bool operator !=(Node obj1, Node obj2) {
			return !(obj1 == obj2);
		}

		public override int GetHashCode() {
			return HashHelper.GetHashCode(NodeName, NodeType, NodeValue);
		}

		public static void Encode(Node obj, BinaryWriter writer) {
			writer.Write(obj.NodeName);
			writer.Write((byte)obj.NodeType);
			switch (obj.NodeType) {
				case NodeType.Byte:
					writer.Write((byte)obj.NodeValue);
					break;
				case NodeType.Short:
					writer.Write((short)obj.NodeValue);
					break;
				case NodeType.Int:
					writer.Write((int)obj.NodeValue);
					break;
				case NodeType.Long:
					writer.Write((long)obj.NodeValue);
					break;
				case NodeType.String:
					writer.Write((string)obj.NodeValue);
					break;
				case NodeType.Bool:
					writer.Write((bool)obj.NodeValue);
					break;
				case NodeType.Node:
					Encode((Node)obj.NodeValue, writer);
					break;
				case NodeType.List:
					var list = (NodeList)obj.NodeValue;
					writer.Write(list.Nodes.Count);
					foreach (Node node in list.Nodes)
						Encode(node, writer);
					break;
			}
			writer.Flush();
		}

		public static Node Decode(BinaryReader reader) {
			string name = reader.ReadString();
			NodeType type = (NodeType)reader.ReadByte();
			object value = null;

			switch (type) {
				case NodeType.Byte:
					value = reader.ReadByte();
					break;
				case NodeType.Short:
					value = reader.ReadInt16();
					break;
				case NodeType.Int:
					value = reader.ReadInt32();
					break;
				case NodeType.Long:
					value = reader.ReadInt64();
					break;
				case NodeType.String:
					value = reader.ReadString();
					break;
				case NodeType.Bool:
					value = reader.ReadBoolean();
					break;
				case NodeType.Node:
					value = Decode(reader);
					break;
				case NodeType.List:
					var nodeList = new List<Node>();
					var nodes = reader.ReadInt32();
					for (int i = 0; i < nodes; i++)
						nodeList.Add(Decode(reader));
					value = new NodeList(nodeList);
					break;
			}

			return new Node(name, type, value);
		}
	}
}
