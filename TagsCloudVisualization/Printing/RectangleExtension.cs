using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Printing
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

        private static Rectangle Resize(this Rectangle source, SizeF delta)
        {
            return new Rectangle(
                (int)(source.X / delta.Width),
                (int)(source.Y / delta.Height),
                (int)(source.Width / delta.Width),
                (int)(source.Height / delta.Height)
            );
        }

        public static Rectangle Translate(this Rectangle source, Size currentSize, Size newSize)
        {
            var sizeDelta = new SizeF(
                (float) currentSize.Width / newSize.Width,
                (float) currentSize.Height / newSize.Height);

            var resized = source.Resize(
                new SizeF(Math.Max(sizeDelta.Height, sizeDelta.Width), 
                Math.Max(sizeDelta.Height, sizeDelta.Width)));
            
            resized = new Rectangle(
                (int)(source.X / sizeDelta.Width),
                (int)(source.Y / sizeDelta.Height),
                (int)(source.Width / Math.Max(sizeDelta.Height, sizeDelta.Width)),
                (int)(source.Height / Math.Max(sizeDelta.Height, sizeDelta.Width))
            );
            
            return resized;
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