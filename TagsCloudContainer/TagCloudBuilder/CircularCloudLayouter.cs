using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly Point center;
        public ICollection<Rectangle> Cloud { get; private set; }
        private IPointsProvider pointsProvider;

        public CircularCloudLayouter(Point center)
        {
            if (center.X <= 0 || center.Y <= 0)
                throw new ArgumentException("Central point coordinates should be in positive");

            this.center = center;

        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("Size width and height should be positive");

            if (Cloud == null || !Cloud.Any())
                return new Rectangle(center, rectangleSize);

            Rectangle rectangle;
            bool placingIsCorrect;

            var enumerator = pointsProvider.Points().GetEnumerator();
            enumerator.MoveNext();

            do
            {
                var point = enumerator.Current;
                enumerator.MoveNext();

                rectangle = new Rectangle(new Point(point.X - rectangleSize.Width / 2,
                        point.Y - rectangleSize.Height / 2),
                    rectangleSize);
                placingIsCorrect = PlacedCorrectly(rectangle, Cloud, new Size(center.X * 2, center.Y * 2));

            } while (!placingIsCorrect);

            return rectangle;
        }

        public void LayoutRectancles(List<Size> rectangleSizes)
        {
            Cloud = new List<Rectangle>();
            pointsProvider = new SpiralPointsProvider(center);

            foreach (var size in rectangleSizes)
                Cloud.Add(PutNextRectangle(size));
        }

        public Image ToImage()
        {
            var image = new Bitmap(center.X * 2, center.Y * 2);
            var gr = Graphics.FromImage(image);
            var pen = new Pen(Color.White);

            gr.Clear(Color.Black);
            gr.DrawRectangles(pen, Cloud.ToArray());

            return image;
        }

        public bool PlacedCorrectly(Rectangle rectangle, ICollection<Rectangle> rectanglesCloud, Size canvasSize)
        {
            if (rectangle.Top < 0 || rectangle.Left < 0 || rectangle.Bottom > canvasSize.Height ||
                rectangle.Right > canvasSize.Width)
                return false;

            foreach (var previous in Cloud)
            {
                if (rectangle.IntersectsWith(previous))
                    return false;
            }

            return true;
        }
    }
}
