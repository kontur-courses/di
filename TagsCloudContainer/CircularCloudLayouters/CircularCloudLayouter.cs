using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.WordsFilters;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public class CircularCloudLayouter : ICircularCloudLayouter
    {
        private readonly IEnumerator<Point> pointsOrder;
        private readonly List<Rectangle> addedRectangles = new List<Rectangle>();
        private readonly IFilter<Rectangle> rectanglesFilter;
        

        public CircularCloudLayouter(IEnumerator<Point> pointsOrder, IFilter<Rectangle> rectanglesFilter)
        {
            this.pointsOrder = pointsOrder;
            this.rectanglesFilter = rectanglesFilter;
        }

        public Rectangle PutNextRectangle(Size size)
        {
            pointsOrder.MoveNext();
            var nextRectangle = GetTheNearestFreeRectangle(size);
            addedRectangles.Add(nextRectangle);
            return nextRectangle;
        }

        private Rectangle GetTheNearestFreeRectangle(Size size)
        {
            while (true)
            {
                var nextRectangle = GetRectangleAtCurrentPoint(size);
                if (rectanglesFilter.IsCorrect(nextRectangle) && !nextRectangle.IntersectsWithPreviousRectangles(addedRectangles))
                    return nextRectangle;
                pointsOrder.MoveNext();
            }
        }

        private Rectangle GetRectangleAtCurrentPoint(Size size)
        {
            var centerPoint = pointsOrder.Current;
            var leftTopPoint = centerPoint.Shift(-size.Width / 2, -size.Height / 2);
            return new Rectangle(leftTopPoint, size);
        }
    }
}