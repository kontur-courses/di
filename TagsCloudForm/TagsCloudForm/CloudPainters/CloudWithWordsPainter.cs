using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CircularCloudLayouter;
using TagsCloudForm.Actions;
using TagsCloudForm.CircularCloudLayouterSettings;
using TagsCloudForm.Common;

namespace TagsCloudForm.CloudPainters
{
    public class CloudWithWordsPainter : ICloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly ICircularCloudLayouterWithWordsSettings settings;
        private readonly IPalette palette;
        private Size imageSize;
        private ICircularCloudLayouter layouter;
        private readonly Dictionary<string, int> words;

        public CloudWithWordsPainter(IImageHolder imageHolder,
            ICircularCloudLayouterWithWordsSettings settings, IPalette palette, ICircularCloudLayouter layouter,
            Dictionary<string,int> words)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
            this.palette = palette;
            this.layouter = layouter;
            this.words = words;

            imageSize = imageHolder.GetImageSize();
        }

        public void Paint()
        {
            var rng = new Random();
            var shuffledWords = settings.Ordered
                ? words.OrderByDescending(x => x.Value)
                : words.OrderBy(a => rng.Next());
            var backgroundBrush = new SolidBrush(palette.SecondaryColor);
            var rectPen = new Pen(palette.PrimaryColor);
            var textBrush = new SolidBrush(palette.PrimaryColor);
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width,
                    imageSize.Height);
                foreach (var word in shuffledWords)
                {
                    DrawRectangle(graphics, word.Key, word.Value, backgroundBrush, rectPen, textBrush);
                }
            }

            imageHolder.UpdateUi();
        }

        private void DrawRectangle(IGraphicDrawer graphics, string word, int wordFrequency, Brush backgroundBrush, Pen rectPen, Brush textBrush)
        {
            var font = new Font("Arial", Math.Min(72, wordFrequency * settings.Scale));
            var size = new Size(TextRenderer.MeasureText(word, font).Width, TextRenderer.MeasureText(word, font).Height);
            var rect = layouter.PutNextRectangle(size);
            if (settings.Fill)
                graphics.FillRectangle(backgroundBrush, rect);
            if (settings.Frame)
                graphics.DrawRectangle(rectPen, rect);
            graphics.DrawString(word, font, textBrush, new PointF(rect.X, rect.Y));
        }
    }
}
