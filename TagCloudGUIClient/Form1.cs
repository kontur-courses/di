using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CloudLayouters;
using TagCloudCreator;

namespace TagCloud
{
    public partial class Form1 : Form
    {
        private readonly CloudPrinter cloudPrinter;
        private readonly List<IColorSelector> colorSelectors;
        private readonly List<BaseCloudLayouter> layouters;
        private Bitmap? image;

        public Form1(TableLayoutPanel table, CloudPrinter cloudPrinter, IEnumerable<BaseCloudLayouter> layouters,
            IEnumerable<IColorSelector> colorSelectors)
        {
            this.layouters = layouters.ToList();
            layouter = this.layouters[0];
            this.table = table;
            this.cloudPrinter = cloudPrinter;
            this.colorSelectors = colorSelectors.ToList();
            InitializeComponent();
        }
    }
}