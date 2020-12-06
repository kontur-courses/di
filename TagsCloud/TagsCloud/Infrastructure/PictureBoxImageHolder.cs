using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Layouters;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Infrastructure
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        public ImageSettings Settings { get; set; }
        private ICloudLayouter layouter;
        private Dictionary<string, int> wordsFreuqencies;
        private IWordsFrequencyParser parser;
        private string previousFileName;

        public PictureBoxImageHolder(IWordsFrequencyParser parser, ImageSettings settings, ICloudLayouter layouter)
        {
            Settings = settings;
            this.layouter = layouter;
            this.parser = parser;
            RecreateCanvas(settings);
        }

        public void ChangeLayouter(ICloudLayouter layouter)
        {
            this.layouter = layouter;
            RedrawCurrentImage();
        }

        private Graphics StartDrawing() => Graphics.FromImage(Image);

        public void RenderWordsFromFile(string fileName)
        {
            if (string.IsNullOrEmpty(previousFileName))
            {
                if (!File.Exists(fileName))
                    throw new FileNotFoundException("Запрошенный файл не найден");
                wordsFreuqencies = parser.ParseWordsFrequencyFromFile(fileName);
                previousFileName = fileName;
            }

            if(wordsFreuqencies is null)
                return;

            layouter.ClearLayouter();
            var totalWords = wordsFreuqencies.Sum(x => x.Value);
            var graphics = StartDrawing();

            foreach (var pair in wordsFreuqencies.OrderByDescending(x => x.Value))
            {
                var fontSize = FontSizeProvider.GetFontSize(Settings.Font.Size, 100 / (double)totalWords * pair.Value / 100);

                var label = new Label {AutoSize = true};
                label.Font = new Font(Settings.Font.FontFamily, fontSize, Settings.Font.Style);
                label.Text = pair.Key;
                var size = label.GetPreferredSize(label.GetPreferredSize(Size));

                Rectangle rect;
                try
                {
                    rect = layouter.PutNextRectangle(size);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK);
                    return;
                }
                
                graphics.DrawString(pair.Key, label.Font, new SolidBrush(Settings.Palette.TextColor), rect);
                UpdateUi();
            }
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateCanvas(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
            layouter.UpdateCenterPoint(imageSettings);
            DrawBaseCanvas();
        }

        public void RedrawCurrentImage()
        {
            RecreateCanvas(Settings);
            RenderWordsFromFile(previousFileName);
        }

        public void SaveImage(string fileName)
        {
            Image.Save(fileName);
        }

        private void DrawBaseCanvas()
        {
            var g = StartDrawing();
            g.FillRectangle(new SolidBrush(Settings.Palette.BackgroundColor),
                new Rectangle(0, 0, Settings.Width, Settings.Height));
            UpdateUi();
        }
    }
}