using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChronosEngine.FileStructure;

namespace FileViewer {
	public partial class Form1 : Form {
		public ChronoFile WorkingFile { get; set; }

		public Form1() {
			InitializeComponent();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			if (MessageBox.Show("Are you sure your want to quit?\nMake sure all of your work is saved.", "Exit application", MessageBoxButtons.YesNo) == DialogResult.No) {
				e.Cancel = true;
			}
		}

		private void fileToolStripMenuItem1_Click(object sender, EventArgs e) {

		}

		private void listToolStripMenuItem_Click(object sender, EventArgs e) {
			using (AddListForm form = new AddListForm()) {
				if (form.ShowDialog() == DialogResult.OK) {
					treeView1.Nodes.Add(new TreeNode(form.ListName, 3, 3));
				}
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e) {
			using (OpenFileDialog ofd = new OpenFileDialog()) {
				ofd.Filter = "Chrono Secure File(.csf)|*.csf|All Files|*.*";
				ofd.InitialDirectory = Directory.GetCurrentDirectory();
				ofd.RestoreDirectory = true;
				if (ofd.ShowDialog() == DialogResult.OK) {
					this.WorkingFile = ChronoFile.Decode(ofd.FileName);
					this.DisplayRoot(WorkingFile.RootNode);
				}
			}
		}

		public void DisplayRoot(Node rootNode) {
			treeView1.Nodes.Clear();
			TreeNode root = GetTreeNode(rootNode);
			treeView1.Nodes.Add(root);
		}

		public TreeNode GetTreeNode(Node node) {
			TreeNode treeNode = new TreeNode(node.NodeName);
			treeNode.ImageIndex = (int)node.NodeType;
			treeNode.Tag = node;

			if (node.NodeType == NodeType.List) {
				NodeList nodeList = (NodeList)node.NodeValue;
				foreach (Node n in nodeList.Nodes)
					treeNode.Nodes.Add(GetTreeNode(n));
			}

			return treeNode;
		}
	}
}
