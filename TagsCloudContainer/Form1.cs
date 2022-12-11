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
        private Bitmap image;
        private Color backgroundColor;
        private Color fontColor;
        private FontFamily fontFamily;
        public Form1()
        {
            InitializeComponent();
            parser = new TextParser();
            ccl = new CircularCloudLayouter();
            drawer = new Drawer();
            BackColor = Color.White;
            fontColor = Color.Black;
            fontFamily = FontFamily.GenericMonospace;
        }

        private void but_fileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
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
                var font = new Font(fontFamily, 15 + count);
                var size = key.MeasureString(font);
                var rect = ccl.GetNextRectangle(new Point(0, 0), rectangles, size);
                rectangles.Add(rect);
                radiusPic = Math.Max(radiusPic, GetMaxCoords(rect));
                containers.Add(new TextContainer(key, rect.GetTopLeftCorner(), font));
            }

            image = drawer.DrawWords(containers, radiusPic, backgroundColor,
                fontColor);
            pic_main.BackgroundImage = image;
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

        private void but_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG picture(*.png)|*.png|JPG picture(*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            image.Save(filename);
        }

        private void but_backgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            colorDialog.Color = backgroundColor;
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            backgroundColor = colorDialog.Color;
        }

        private void but_fontColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.FullOpen = true;
            colorDialog.Color = fontColor;
            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;
            fontColor = colorDialog.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.Cancel)
                return;
            Font font = fontDialog.Font;
            fontFamily = font.FontFamily;
        }
    }
}