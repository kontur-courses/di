using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using TagsCloudVisualization.TextFormatters;

namespace TagsCloudVisualization
{
    public class Painter
    {
        private readonly Size size;

        public Painter(Size size)
        {
            this.size = size;
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
                    g.DrawString(word.Value, word.Font, new SolidBrush(Color.Aqua), word.Rectangle, stringFormat);
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
            b.Save(path);
        }
    }
}