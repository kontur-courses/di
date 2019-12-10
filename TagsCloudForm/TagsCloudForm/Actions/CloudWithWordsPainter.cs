using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudForm.CircularCloudLayouterSettings;
using TagsCloudForm.Common;
using CircularCloudLayouter;

namespace TagsCloudForm.Actions
{
    public class CloudWithWordsPainter : ICloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly CircularCloudLayouterWithWordsSettings settings;
        private readonly IPalette palette;
        private Size imageSize;
        private ICircularCloudLayouter layouter;
        private readonly Dictionary<string, int> words;

        public CloudWithWordsPainter(IImageHolder imageHolder,
            CircularCloudLayouterWithWordsSettings settings, IPalette palette, ICircularCloudLayouter layouter,
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
            using (var graphics = imageHolder.StartDrawing())
            {
                graphics.FillRectangle(new SolidBrush(palette.BackgroundColor), 0, 0, imageSize.Width,
                    imageSize.Height);
                var backgroundBrush = new SolidBrush(palette.SecondaryColor);
                var rectBrush = new Pen(palette.PrimaryColor);
                var rng = new Random();
                IOrderedEnumerable<KeyValuePair<string, int>> shuffledWords;
                if (settings.Ordered)
                    shuffledWords = words.OrderByDescending(x => x.Value);
                else
                    shuffledWords = words.OrderBy(a => rng.Next());
                foreach (var word in shuffledWords)
                {
                    var font = new Font("Arial", Math.Min(72, word.Value * settings.Scale));
                    var size = new Size(TextRenderer.MeasureText(word.Key, font).Width, TextRenderer.MeasureText(word.Key, font).Height);
                    var rect = layouter.PutNextRectangle(size);
                    graphics.FillRectangle(backgroundBrush, rect);
                    graphics.DrawRectangle(rectBrush, rect);
                    graphics.DrawString(word.Key, font, new SolidBrush(palette.PrimaryColor), new PointF(rect.X, rect.Y));
                }
            }

            imageHolder.UpdateUi();
        }
    }
}
