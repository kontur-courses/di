using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Extensions;

namespace TagsCloud.Layouter.Base
{
    public abstract class LayouterBase : ILayouter
    {
        private readonly List<Rectangle> rectangles;
        private readonly Point center;

        public LayouterBase(Point center)
        {
            this.center = center;
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = new Rectangle();
            rectangle.Size = rectangleSize;
            foreach (var point in GetPoints())
            {
                rectangle.Location = point;
                if (!rectangle.IntersectsWith(rectangles))
                    break;
            }
            var movedRectangle = MoveToCenter(rectangle);
            rectangles.Add(movedRectangle);
            return movedRectangle;
        }

        private Rectangle MoveToCenter(Rectangle rectangle)
        {
            var targetVector = new TargetVector(center, rectangle.Location);
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
                size.Width = Math.Max(rect.X - center.X + rect.Width, size.Width);
                size.Height = Math.Max(rect.Y - center.Y + rect.Height, size.Height);
            }
            size.Height += center.X * 2;
            size.Width += center.Y * 2;
            return size;
        }

        protected abstract IEnumerable<Point> GetPoints();
    }
}
