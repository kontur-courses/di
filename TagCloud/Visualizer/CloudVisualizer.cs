using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using TagCloud.Extensions;
using TagCloud.Models;
using TagCloud.Visualizer.Settings;

namespace TagCloud.Visualizer
{
    public class CloudVisualizer : ICloudVisualizer
    {
        public IDrawSettings Settings { get; set; }
        private Graphics graphics;

        public CloudVisualizer(IDrawSettings settings)
        {
            Settings = settings;
        }

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
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Settings.Color);
                graphics.TranslateTransform(-bounds.X, -bounds.Y);
                Draw(cloudItems);
            }
            return picture;
        }

        private void Draw(CloudItem[] cloudItems)
        {
            var bounds = cloudItems.Select(t => t.Bounds).ToArray();
            var words = cloudItems.Select(t => t.Word).ToArray();
            if (Settings.DrawFormat != DrawFormat.OnlyWords)
                graphics.DrawRectangles(Pens.Black, bounds);
            if (Settings.DrawFormat == DrawFormat.OnlyWords || Settings.DrawFormat == DrawFormat.WordsInRectangles)
                for (var i = 0; i < words.Length; i++)
                    DrawAsString(words[i], bounds[i]);
            if (Settings.DrawFormat == DrawFormat.RectanglesWithNumeration)
                for (var i = 0; i < words.Length; i++)
                {
                    var word = i.ToString();
                    DrawAsString(word, bounds[i]);
                }
        }

        private Font GetSuitableFontFor(string word, Rectangle bounds)
        {
            var font = Settings.Font;
            var stringSize = graphics.MeasureString(word, font);

            while (stringSize.Height < bounds.Height && stringSize.Width < bounds.Width)
            {
                font = new Font(font.FontFamily, font.Size + 1);
                stringSize = graphics.MeasureString(word, font);
            }

            return new Font(font.FontFamily, font.Size - 1);
        }

        private void DrawAsString(string str, Rectangle bounds)
        {
            var cloudItem = new CloudItem(str,bounds);
            var font = GetSuitableFontFor(str, bounds);
            graphics.DrawString(
                str,
                font,
                Settings.Colorizer.GetBrush(cloudItem),
                bounds);
        }
    }
}