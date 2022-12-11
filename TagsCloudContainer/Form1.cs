using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudContainer
{
    public partial class Form1 : Form
    {
        private string fileText;
        private TextParser parser;
        private CircularCloudLayouter ccl;
        private Drawer drawer;
        public Form1()
        {
            InitializeComponent();
            parser = new TextParser();
            ccl = new CircularCloudLayouter();
            drawer = new Drawer();
            comboBox_backgroundColor.SelectedItem = System.Drawing.Color.White;
            comboBox_textColor.SelectedItem = System.Drawing.Color.Black;
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
            var parsedText = parser.Parse(fileText);
            var rectangles = new List<Rectangle>();
            var containers = new List<TextContainer>();
            var radiusPic = 0;
            ccl.StepSize = GetStepSize();
            foreach (var key in parsedText.Keys)
            {
                int count = parsedText[key];
                var font = new Font(FontFamily.GenericMonospace, 15 + count);
                var size = key.MeasureString(font);
                var rect = ccl.GetNextRectangle(new Point(0, 0), rectangles, size);
                rectangles.Add(rect);
                radiusPic = Math.Max(radiusPic, GetMaxCoords(rect));
                containers.Add(new TextContainer(key, rect.GetTopLeftCorner(), font));
            }

            var bitmap = drawer.DrawWords(containers, radiusPic, (Color)comboBox_backgroundColor.SelectedItem,
                (Color)comboBox_textColor.SelectedItem);
            pic_main.BackgroundImage = bitmap;
        }

        private int GetStepSize()
        {
            int stepSize = 0;
            try
            {
                stepSize = int.Parse(box_stepSize.Text);
            }
            catch
            {
                stepSize = 0;
            }

            return stepSize <= 0 ? 0 : stepSize;
        }

        private int GetMaxCoords(Rectangle rect)
        {
            int max = Math.Max(Math.Abs(rect.X), Math.Abs(rect.Y));
            max = Math.Max(max, Math.Abs(rect.Bottom));
            max = Math.Max(max, Math.Abs(rect.Top));
            return max;
        }
    }
}