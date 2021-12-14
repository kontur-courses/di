using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public static class RectangleExtension
    {
        public static Rectangle Move(this Rectangle source, Point delta)
        {
            return Move(source, delta.X, delta.Y);
        }
        
        public static Rectangle Move(this Rectangle source, int xDelta, int yDelta)
        {
            return new Rectangle(source.X + xDelta, source.Y + yDelta, source.Width, source.Height);
        }

        public static Rectangle Translate(this Rectangle source, Size currentSize, Size newSize)
        {
            var sizeDelta = new SizeF(
                ((float) currentSize.Width / newSize.Width),
                ((float) currentSize.Height / newSize.Height));

            return new Rectangle(
                (int)(source.X / sizeDelta.Width),
                (int)(source.Y / sizeDelta.Height),
                (int)(source.Width / sizeDelta.Width),
                (int)(source.Height / sizeDelta.Height)
            );
        }
        
        internal static Size GetCircumscribedSize(this IList<Rectangle> rectangles)
        {
            return new Size(
                Math.Abs(rectangles.Max(x => x.Right) - rectangles.Min(x => x.Left)),
                Math.Abs(rectangles.Max(x => x.Bottom) - rectangles.Min(x => x.Top))
            );
        }
    }
}