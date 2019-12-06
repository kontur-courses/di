using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class CircularCloudLayouter : ILayouter
    {
        public readonly Point Center;

        private readonly List<Rectangle> rectangles;

        public IEnumerable<Rectangle> Rectangles => new List<Rectangle>(rectangles);


        private readonly Spiral spiral;
        private double spiralParam;

        public int LayoutHeight => 2 * rectangles.Max(r => Math.Abs(r.Y));
        public int LayoutWidth => 2 * rectangles.Max(r => Math.Abs(r.X));

        public CircularCloudLayouter(Spiral spiral)
        {
            this.spiral = spiral;
            Center = new Point(0,0);
            rectangles = new List<Rectangle>();
        }

        public TagCloudItem PlaceNextWord(WordData word, Size size)
        {
            return new TagCloudItem(PutNextRectangle(size), word.Word);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            if (rectangles.Count == 0)
            {
                var location = new Point(Center.X - rectangleSize.Width / 2,
                    Center.Y - rectangleSize.Height / 2);
                rectangle = new Rectangle(location, rectangleSize);
            }
            else
            {
                rectangle = new Rectangle(new Point(0, 0), rectangleSize);
                PlaceOnSpiral(ref rectangle);
                SealInLayout(ref rectangle);
            }

            rectangles.Add(rectangle);

            return rectangle;
        }

        private void PlaceOnSpiral(ref Rectangle rectangle)
        {
            while (RectangleIntersectsWithLayout(rectangle))
            {
                var newRectCenter = spiral.Calculate(spiralParam);
                rectangle.Location = new Point(newRectCenter.X - rectangle.Size.Width / 2,
                    newRectCenter.Y - rectangle.Size.Height / 2);
                spiralParam += 0.1;
            }
        }

        private void SealInLayout(ref Rectangle rectangle)
        {
            var rectCenter = rectangle.GetCenter();
            var dx = Math.Sign(Center.X - rectCenter.X);
            var dy = Math.Sign(Center.Y - rectCenter.Y);
            var done = false;
            while (!done)
            {
                rectangle.X += dx;
                rectangle.Y += dy;

                if (!RectangleIntersectsWithLayout(rectangle))
                    continue;

                rectangle.X -= dx;
                rectangle.Y -= dy;
                done = true;
            }
        }

        private bool RectangleIntersectsWithLayout(Rectangle rectangle)
        {
            return rectangles.Select(rectangle.IntersectsWith).Any(r => r);
        }
    }

    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2,
                rect.Top + rect.Height / 2);
        }
    }
}