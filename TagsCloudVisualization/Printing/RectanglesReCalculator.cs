using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Printing
{
    public class RectanglesReCalculator : IRectanglesReCalculator
    {
        public IList<Rectangle> RecalculateRectangles(IList<Rectangle> rectangles, Size defaultMaxSize)
        {
            var centeredRects = MoveToCenter(rectangles);
            var oldSize = centeredRects.GetCircumscribedSize();
            return centeredRects.Select(x => x.Translate(oldSize, defaultMaxSize)).ToList();
        }

        public IList<Rectangle> MoveToCenter(IList<Rectangle> rectangles)
        {
            if (!rectangles.Any()) throw new ArgumentException($"rectangles list is empty");
            
            var rectangle = rectangles.First();
            var center = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            var initialSize = rectangles.GetCircumscribedSize();
            var centersDelta = new Size(center.X - initialSize.Width / 2, center.Y - initialSize.Height / 2);

            return rectangles.Select(x => x.Move(-centersDelta.Width, -centersDelta.Height)).ToList();
        }
    }
}