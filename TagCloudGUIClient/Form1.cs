using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using TagCloudCreator;

namespace TagCloudGUIClient
{
    public partial class Form1 : Form
    {
        private readonly CloudPrinter cloudPrinter;
        private readonly List<IColorSelectorFabric> colorSelectors;
        private readonly List<IBaseCloudLayouterFabric> layouters;


        public Form1(CloudPrinter cloudPrinter, IEnumerable<IColorSelectorFabric> colorSelectors,
            IEnumerable<IBaseCloudLayouterFabric> layouters)
        {
            InitializeComponent();
            fontSelector.Items.AddRange(FontFamily.Families.Select(x => x.Name).ToArray());
            this.colorSelectors = colorSelectors.ToList();
            this.layouters = layouters.ToList();
            layouterSelector.Items.AddRange(this.layouters.Select(x => x.Name).ToArray());
            colorSelectorSelector.Items.AddRange(this.colorSelectors.Select(x => x.Name).ToArray());
            this.cloudPrinter = cloudPrinter;
        }

        private Size ImageSize { get; set; }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            RedrawImage();
        }


        private void fileSelector_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"files with text|*.txt;*.doc;*.docx;*.html;*.pdf";
            if (openFileDialog.ShowDialog() == DialogResult.OK) textBox1.Text = openFileDialog.FileName;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = @"png files (*.png)|*.png";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var stream = saveFileDialog.OpenFile();
            pictureBox1.Image.Save(stream, ImageFormat.Png);
            stream.Close();
        }

        private void RedrawImage()
        {
            if (!AllSelected())
                return;
            var fontFamily = FontFamily.Families[fontSelector.SelectedIndex];
            var colorSelector = colorSelectors.First(x => x.Name == (string) colorSelectorSelector.SelectedItem)
                .Create();
            var layouter = layouters.First(x => x.Name == (string) layouterSelector.SelectedItem)
                .Create(new Point(ImageSize / 2));
            var path = textBox1.Text;
            pictureBox1.Image = cloudPrinter.DrawCloud(path,
                layouter,
                ImageSize,
                fontFamily,
                colorSelector);
        }

        private bool AllSelected()
        {
            return fontSelector.SelectedIndex != -1
                   && layouterSelector.SelectedIndex != -1
                   && colorSelectorSelector.SelectedIndex != -1
                   && textBox1.Text != "";
        }

        private void sizeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pair = ((string) sizeSelector.SelectedItem).Split('x');
            ImageSize = new Size(int.Parse(pair[0]), int.Parse(pair[1]));
        }
    }
}