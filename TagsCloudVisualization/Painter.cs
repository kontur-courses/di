using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using TagsCloudVisualization.TextFormatters;

namespace TagsCloudVisualization
{
    public class Painter : IPainter
    {
        private readonly Size size;
        private IEnumerable<Color> colors;
        private readonly IList<Color> defaultColors = new List<Color> { Color.Aqua };
        private Random rnd;

        private Color RandomColor
        {
            get
            {
                var colorsList = colors.ToList();
                return colorsList.Count == 0
                    ? defaultColors[rnd.Next(defaultColors.Count)]
                    : colorsList[rnd.Next(colorsList.Count)];
            }
        }

        public Painter(Size size) : this(size, new List<Color>())
        {

        }

        public Painter(Size size, IEnumerable<Color> colors)
        {
            this.size = size;
            this.colors = colors;
            rnd = new Random();
        }

        public List<Word> TransformWords(List<Word> rectangles, Size newCanvas)
        {
            rectangles.ForEach(t =>
                t.Rectangle = new Rectangle(CalcPositionForCanvas(t.Rectangle.Location, newCanvas), t.Rectangle.Size));
            return rectangles;
        }

        public List<Rectangle> TransformRectangles(List<Rectangle> rectangles, Size newCanvas)
        {
            return rectangles.Select(t =>
                new Rectangle(CalcPositionForCanvas(t.Location, newCanvas), t.Size)
            ).ToList();
        }

        private Point CalcPositionForCanvas(Point position, Size imageSize)
        {
            var x = position.X + imageSize.Width / 2;
            var y = position.Y + imageSize.Height / 2;
            return new Point(x, y);
        }

        public void DrawWordsToFile(List<Word> words, string path)
        {
            words = TransformWords(words, size);
            var b = new Bitmap(size.Width, size.Height);

            using (var g = Graphics.FromImage(b))
            {
                foreach (var word in words)
                {
                    var stringFormat = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    };
                    g.DrawString(word.Value, word.Font, new SolidBrush(RandomColor), word.Rectangle, stringFormat);
                }
            }
            b.Save(path);
        }

        public void DrawRectanglesToFile(List<Rectangle> rectangles, string path)
        {
            rectangles = TransformRectangles(rectangles, size);
            var b = new Bitmap(size.Width, size.Height);

            using (var g = Graphics.FromImage(b))
            {
                rectangles.ForEach(t => g.DrawRectangle(new Pen(Brushes.DeepSkyBlue), t));
            }
            b.Save(path, GetImageFormat(path));
        }

        public static ImageFormat GetImageFormat(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return extension.ToLower() switch
            {
                ".png" => ImageFormat.Png,
                ".bmp" => ImageFormat.Bmp,
                ".ico" => ImageFormat.Icon,
                ".ic" => ImageFormat.Icon,
                ".jpg" => ImageFormat.Jpeg,
                ".jpeg" => ImageFormat.Jpeg,
                _ => throw new ArgumentException($"Extension {extension} are not supported!")
            };
        }
    }
}