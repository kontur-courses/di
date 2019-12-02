using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly List<Rectangle> arrangedRectangles = new List<Rectangle>();
        private readonly IEnumerator<Point> spiralEnumerator;

        public CircularCloudLayouter(ArchimedesSpiral spiral)
        {
            if(spiral == null)
                throw new ArgumentException("Tag Cloud spiral can't be null.");

            spiralEnumerator = spiral.GetEnumerator();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Tags Cloud rectangle size parameters should be positive.");
            
            var tempRect = new Rectangle(spiralEnumerator.Current, rectangleSize);
            while (arrangedRectangles.Any(r => r.IntersectsWith(tempRect)) && spiralEnumerator.MoveNext())
                tempRect.Location = spiralEnumerator.Current;
            arrangedRectangles.Add(tempRect);
            return tempRect;
        }
    }
}