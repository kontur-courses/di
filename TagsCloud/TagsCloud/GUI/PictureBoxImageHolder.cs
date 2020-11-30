using System.Collections.Generic;
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
        private Dictionary<string, int> wordsFreuqencies = new Dictionary<string, int>();

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
            var graphics = StartDrawing();
            foreach (var pair in frequencyDictionary.OrderByDescending(x => x.Value))
            {
                var label = new Label { AutoSize = true, Text = pair.Key };
                label.Refresh();
                var rect = layouter.PutNextRectangle(label.Size);
                graphics.DrawString(pair.Key, Settings.Font, new SolidBrush(Settings.Palette.TextColor), rect);
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