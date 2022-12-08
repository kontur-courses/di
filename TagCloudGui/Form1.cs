using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagCloud.Readers;
using TagCloud.TagCloudVisualizations;

namespace TagCloudGui
{
    public partial class MainForm : Form
    {
        private ITagCloudVisualizationSettings imageSettings;
        public MainForm()
        {
            imageSettings = TagCloudVisualizationSettings.Default();
            InitializeComponent();
        }

        private void loadWordsMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
