using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;

namespace TagsCloudVisualization
{
    public class MainForm : Form
    {
        private Image image;
        private IFileReader fileReader;
        private ICloudLayouter cloudLayouter;
        private IVisualizer visualizer;
        private List<GraphicWord> words;
        private WordCounter counter = new WordCounter();

        public MainForm(IFileReader fileReader, ICloudLayouter cloudLayouter, IVisualizer visualizer)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Size = new Size(616, 664);
            this.fileReader = fileReader;
            this.cloudLayouter = cloudLayouter;
            this.visualizer = visualizer;

            var openButton = new Button {Text = "Open file", Size = new Size(200, 32)};
            openButton.Click += OpenFile;
            Controls.Add(openButton);

            var generateButton = new Button {Text = "Generate", Location = new Point(200, 0), Size = new Size(200, 32) };
            generateButton.Click += GenerateImage;
            Controls.Add(generateButton);

            var saveButton = new Button {Text = "Save to file", Location = new Point(400, 0), Size = new Size(200, 32) };
            saveButton.Click += SaveFile;
            Controls.Add(saveButton);
        }

        private void OpenFile(object sender, EventArgs args)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text file | *.txt; ";
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            var path = dialog.FileName;
            words = counter.Count(fileReader.Read(path));
        }

        private void GenerateImage(object sender, EventArgs args)
        {
            if (words == null)
            {
                MessageBox.Show("No word file");
                return;
            }

            cloudLayouter.Clear();
            foreach (var word in words)
                cloudLayouter.PutNextWord(word);

            image = visualizer.Render(words, 600, 600);
            Invalidate();
        }

        private void SaveFile(object sender, EventArgs args)
        {
            if (image == null)
            {
                MessageBox.Show("Generate image first");
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.Filter = "BMP | *.bmp| PNG | *.png;";
            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            var path = dialog.FileName;
            ImageSaver.WriteToFile(path, image);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (image != null)
                e.Graphics.DrawImage(image, new Point(0, 32));
        }
    }
}
