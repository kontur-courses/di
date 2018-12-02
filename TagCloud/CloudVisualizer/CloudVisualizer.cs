using System;
using System.Drawing;
using System.Linq;
using TagCloud.Extensions;
using TagCloud.Models;

namespace TagCloud.CloudVisualizer
{
    public class CloudVisualizer : ICloudVisualizer
    {
        public DrawSettings Settings { get; set; }
        private Graphics graphics;

        public Bitmap CreatePictureWithItems(CloudItem[] cloudItems)
        {
            if (cloudItems == null)
                throw new ArgumentException("Array can't be null");
            if (cloudItems.Length == 0)
                throw new ArgumentException("Array can't be empty");

            var bounds = cloudItems
                .SelectMany(tag => new[] { tag.Bounds.Location, new Point(tag.Bounds.Right, tag.Bounds.Bottom) })
                .ToArray()
                .GetBounds();
            var picture = new Bitmap(bounds.Width, bounds.Height);

            using (graphics = Graphics.FromImage(picture))
            {
                graphics.TranslateTransform(-bounds.X, -bounds.Y);
                Draw(cloudItems);
            }
            return picture;
        }

        private void Draw(CloudItem[] cloudItems)
        {
            var bounds = cloudItems.Select(t => t.Bounds).ToArray();
            var words = cloudItems.Select(t => t.Word).ToArray();
            if (Settings != DrawSettings.OnlyWords)
                graphics.DrawRectangles(Pens.Black, bounds);
            if (Settings == DrawSettings.OnlyWords || Settings == DrawSettings.WordsInRectangles)
                for (var i = 0; i < words.Length; i++)
                    DrawString(words[i], bounds[i]);
            if (Settings == DrawSettings.RectanglesWithNumeration)
                for (var i = 0; i < words.Length; i++)
                {
                    var word = i.ToString();
                    DrawString(word, bounds[i]);
                }
        }

        private Font GetSuitableFontFor(CloudItem cloudItem)
        {
            var bounds = cloudItem.Bounds;
            var word = cloudItem.Word;
            var font = new Font("Arial", 15);
            var stringSize = graphics.MeasureString(word, font);

            while (stringSize.Height < bounds.Height && stringSize.Width < bounds.Width)
            {
                font = new Font("Arial", font.Size + 1);
                stringSize = graphics.MeasureString(word, font);
            }

            return new Font("Arial", font.Size - 1);
        }

        private void DrawString(string str, Rectangle bounds)
        {
            var font = GetSuitableFontFor(new CloudItem(str, bounds));
            graphics.DrawString(
                str,
                font,
                Brushes.Black,
                bounds);
        }
    }
}