using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Layouter
{
    public abstract class CloudLayouter : ICloudLayouter
    {
        public int Count => rectangles.Count;

        protected readonly List<Rectangle> rectangles;

        protected CloudLayouter()
        {
            rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size size)
        {
            if (size.Height <= 0 || size.Width <= 0)
                throw new ArgumentException($"Wrong size: W:{size.Width} H:{size.Height}");

            var rectangle = GetNextRectangle(size);

            rectangles.Add(rectangle);

            return rectangle;
        }

        protected bool IsInsideSurface(Rectangle foundRectangle)
        {
            return rectangles.Any(rect =>
                rect.IntersectsWith(foundRectangle));
        }

        protected abstract Rectangle GetNextRectangle(Size size);
    }
}