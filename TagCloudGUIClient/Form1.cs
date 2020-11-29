using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CloudLayouters;

namespace TagCloud
{
    public partial class Form1 : Form
    {
        private readonly Bitmap image;
        private readonly List<BaseCloudLayouter> layouters;

        public Form1(Bitmap image, TableLayoutPanel table, IEnumerable<BaseCloudLayouter> layouters)
        {
            this.image = image;
            this.layouters = layouters.ToList();
            this.table = table;
            InitializeComponent();
        }
    }
}