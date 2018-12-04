using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization.CloudGenerating
{
    public class CircularCloudLayouter : ILayouter
    {
        private readonly List<Rectangle> placedRectangles;
        public IReadOnlyList<Rectangle> PlacedRectangles => placedRectangles.AsReadOnly();
        private readonly ISpiralGenerator spiralGenerator;

        public CircularCloudLayouter(ISpiralGenerator spiralGenerator)
        {
            placedRectangles = new List<Rectangle>();
            this.spiralGenerator = spiralGenerator;
        }
       
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle nextRectangle;
            do
            {
                var nextPoint = spiralGenerator.GetNextPoint();
                nextRectangle = GetRectangleWithCenterIn(
                    new Point((int)nextPoint.X, (int)nextPoint.Y), rectangleSize);
            } while (nextRectangle.IntersectsWithAny(placedRectangles));

            placedRectangles.Add(nextRectangle);
            return nextRectangle;
        }

        private Rectangle GetRectangleWithCenterIn(Point rectangleCenter, Size rectangleSize)
        {
            return new Rectangle(
                new Point(rectangleCenter.X - rectangleSize.Width / 2,
                          rectangleCenter.Y - rectangleSize.Height / 2), rectangleSize);
        }
    }
}
