using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.CloudLayouter;
using Font = System.Drawing.Font;

namespace TagsCloudGenerator.Visualizer
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private readonly IColoringAlgorithm coloringAlgorithm;

        public CloudVisualizer(IColoringAlgorithm coloringAlgorithm)
        {
            this.coloringAlgorithm = coloringAlgorithm;
        }

        public Bitmap Draw(Cloud cloud, ImageSettings settings)
        {
            var words = cloud.Words;

            if (words.Count == 0)
                throw new ArgumentException("There are no words");

            var bitmap = new Bitmap(settings.Width, settings.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            using (var colors = coloringAlgorithm.GetColors(settings).GetEnumerator())
            {
                graphics.Clear(settings.BackgroundColor);
                graphics.TranslateTransform(settings.Width / 2f, settings.Height / 2f);

                var scale = ComputeScale(words, settings);
                graphics.ScaleTransform(scale, scale);

                foreach (var word in words)
                {
                    colors.MoveNext();
                    var color = colors.Current;
                    var brush = new SolidBrush(color);
                    var font = new Font(settings.Font.FontFamily, settings.Font.Size * word.Count);
                    graphics.DrawString(word.Value, font, brush, word.Rectangle);
                }
            }

            return bitmap;
        }

        private static float ComputeScale(IReadOnlyCollection<Word> words, ImageSettings settings)
        {
            var widthScale = settings.Width / words.Average(x => x.Rectangle.Width) / (words.Count / 3f);
            var heightScale = settings.Height / words.Average(x => x.Rectangle.Height) / (words.Count / 3f);

            return Math.Min((float) widthScale, (float) heightScale);
        }
    }
}