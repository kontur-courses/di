using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.CloudConstruction.Exceptions;
using TagsCloud.CloudConstruction.Extensions;

namespace TagsCloud.CloudConstruction
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        public readonly Point Center;
        private const int Frequency = 36;

        public List<Rectangle> Rectangles { get; set; }

        public CircularCloudLayouter(Point center)
        {
            this.Center = center;
            Rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException(rectangleSize.ToString());
            var rectangle = GenerateRectangle(rectangleSize);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GenerateRectangle(Size rectSize)
        {
            try
            {
                var center = SpiralPointGenerator()
                    .First(centerPoint =>
                        !NewRectangleIntersectsWithAny(centerPoint, rectSize));
                return new Rectangle(ConvertToLocation(center, rectSize), rectSize);
            }
            catch (InvalidOperationException)
            {
                throw new SpiralPointGeneratorException();
            }
        }

        private bool NewRectangleIntersectsWithAny(Point rectCenter, Size rectSize)
        {
            var rect = new Rectangle(ConvertToLocation(rectCenter, rectSize), rectSize);
            return rect.IntersectsWithAny(Rectangles);
        }

        private static Point ConvertToLocation(Point center, Size recSize)
        {
            return new Point(center.X - recSize.Width / 2, center.Y - recSize.Height / 2);
        }

        private IEnumerable<Point> SpiralPointGenerator()
        {
            var angle = 0.0;
            var curPoint = Center;
            while (true)
            {
                yield return curPoint;
                angle += Math.PI / Frequency;

                var x = (int) (angle * Math.Cos(angle) - Center.X);
                var y = (int) (angle * Math.Sin(angle) - Center.Y);
                curPoint = new Point(x, y);
            }
        }
    }
}