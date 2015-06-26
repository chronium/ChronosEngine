using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileViewer {
	public partial class AddListForm : Form {
		public string ListName { get; set; }

		public AddListForm() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			ListName = textBox1.Text;
			this.DialogResult = DialogResult.OK;
        }
	}
}
