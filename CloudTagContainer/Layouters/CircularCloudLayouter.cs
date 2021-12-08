using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CloudTagContainer
{
    public class CircularCloudLayouter : ILayouter
    {
        public Point Center { get; set; }
        
        private List<Rectangle> puttedRectangles = new();
        private readonly ISpiral spiral;
        private IEnumerator<Point> spiralEnumerator;

        public CircularCloudLayouter(ISpiral spiral)
        {
            this.spiral = spiral;
        }

       
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            spiralEnumerator ??= spiral.GetEnumerator(Center).GetEnumerator();
            
            while (spiralEnumerator.MoveNext())
            {
                var potentialCenter = spiralEnumerator.Current;
                var newRectangle = new Rectangle(potentialCenter - rectangleSize / 2, rectangleSize);
                if (puttedRectangles.All(x => !x.IntersectsWith(newRectangle)))
                {
                    puttedRectangles.Add(newRectangle);
                    return newRectangle;
                }
            }

            throw new Exception("Was not able to find next rectangle");
        }
    }
}