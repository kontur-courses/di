﻿using System;
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
        private WordsFilter wordsFilter;
        public Form1()
        {
            InitializeComponent();
            parser = new TextParser();
            ccl = new CircularCloudLayouter();
            drawer = new Drawer();
            wordsFilter = new WordsFilter();
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
            ccl.StepSize = TryToParse(box_stepSize.Text);
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

        private int GetMaxCoords(Rectangle rect)
        {
            int max = Math.Max(Math.Abs(rect.X), Math.Abs(rect.Y));
            max = Math.Max(max, Math.Abs(rect.Bottom));
            max = Math.Max(max, Math.Abs(rect.Top));
            return max;
        }

        private void but_save_Click(object sender, EventArgs e)
        {
            int heigth = TryToParse(box_heightPic.Text);
            int width = TryToParse(box_widthPic.Text);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG picture(*.png)|*.png|JPG picture(*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog.FileName;
            if (heigth > 0 && width > 0)
            {
                image = new Bitmap(image, new Size(width, heigth));
            }
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

        private int TryToParse(string str)
        {
            int number;
            try
            {
                number = int.Parse(str);
            }
            catch
            {
                number = 0;
            }

            return number;
        }

        private void but_openExludedWordForm_Click(object sender, EventArgs e)
        {
            var form = new ExludedWordsForm(wordsFilter);
            form.Show();
        }
    }
}