using System.Collections.Generic;
using System.Drawing;
using TagsCloud2;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ILayouter
    {

        private Point center;
        private List<Rectangle> pastRectangles;
        
        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            pastRectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var spiralPoints = new SpiralPoints(center, rectangleSize.Height);
            while (true)
            {
                spiralPoints.MoveNext();
                var nextCenterPoint = spiralPoints.Current;
                var newRectangle = new Rectangle(
                    nextCenterPoint.X - rectangleSize.Width/2,
                    nextCenterPoint.Y - rectangleSize.Height/2,
                    rectangleSize.Width,
                    rectangleSize.Height
                    );
                var isPositionWithoutCrossing = true;
                for (int i = 0; i < pastRectangles.Count; i++)
                {
                    var intersection = Rectangle.Intersect(pastRectangles[i], newRectangle);
                    if (intersection != Rectangle.Empty)
                    {
                        isPositionWithoutCrossing = false;
                        break;
                    }
                }

                if (isPositionWithoutCrossing)
                {
                    pastRectangles.Add(newRectangle);
                    return newRectangle;
                }
            }
        }
    }
}