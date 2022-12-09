using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudContainer
{
    public partial class Form1 : Form
    {
        private string fileText;
        public Form1()
        {
            InitializeComponent();
        }

        private void but_fileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = ofd.FileName;
            fileText = System.IO.File.ReadAllText(filename);
        }

        private void but_generate_Click(object sender, EventArgs e)
        {

        }
    }
}