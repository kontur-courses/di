using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Extensions;

namespace TagsCloud.Layouter
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly List<Rectangle> rectangles;
        private Spiral spiral;

        public CircularCloudLayouter()
        {
            rectangles = new List<Rectangle>();
            spiral = new Spiral(new Point(0, 0));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle();
            rectangle.Size = rectangleSize;
            foreach (var point in spiral.GetPoints())
            {
                rectangle.Location = point;
                if (!rectangle.IntersectsWith(rectangles))
                    break;
            }
            var movedRectangle = MoveToCenter(rectangle);
            rectangles.Add(movedRectangle);
            return movedRectangle;
        }

        internal RectangleF[] ToArray()
        {
            throw new NotImplementedException();
        }

        private Rectangle MoveToCenter(Rectangle rectangle)
        {
            var targetVector = new TargetVector(spiral.Center, rectangle.Location);
            foreach (var delta in targetVector.GetPartialDelta())
            {
                var newRectangle = rectangle.MoveOnTheDelta(delta);
                if (newRectangle.IntersectsWith(rectangles))
                    continue;
                rectangle = newRectangle;
            }
            return rectangle;
        }

        public Size GetLayoutSize()
        {
            var size = new Size();
            foreach (var rect in rectangles)
            {
                size.Width = Math.Max(rect.X - spiral.Center.X + rect.Width, size.Width);
                size.Height = Math.Max(rect.Y - spiral.Center.Y + rect.Height, size.Height);
            }
            size.Height += spiral.Center.X * 2;
            size.Width += spiral.Center.Y * 2;
            return size;
        }

        public void SetCenter(Point center)
        {
            spiral = new Spiral(center);
        }
    }
}
