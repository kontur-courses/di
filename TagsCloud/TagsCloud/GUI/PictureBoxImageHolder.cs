﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloud
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        public ImageSettings Settings { get; set; }
        private ICloudLayouter layouter;
        private Dictionary<string, int> wordsFreuqencies;

        public PictureBoxImageHolder(ImageSettings settings, ICloudLayouter layouter)
        {
            Settings = settings;
            this.layouter = layouter;
            RecreateCanvas(settings);
        }

        public Graphics StartDrawing() => Graphics.FromImage(Image);

        public void RenderWords(Dictionary<string, int> frequencyDictionary)
        {
            layouter.ClearLayouter();
            wordsFreuqencies = frequencyDictionary;
            var totalWords = frequencyDictionary.Count;
            var graphics = StartDrawing();

            foreach (var pair in frequencyDictionary.OrderByDescending(x => x.Value))
            {
                var fontSize = FontSizeProvider.GetFontSize(Settings.Font.Size, (double)totalWords * pair.Value / 100);

                var label = new Label {AutoSize = true};
                label.Font = new Font(Settings.Font.FontFamily, fontSize, Settings.Font.Style);
                label.Text = pair.Key;
                var size = label.GetPreferredSize(label.GetPreferredSize(Size));

                var rect = layouter.PutNextRectangle(size);
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
            RenderWords(wordsFreuqencies);
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