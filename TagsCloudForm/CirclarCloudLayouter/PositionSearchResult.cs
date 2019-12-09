using System.Drawing;

namespace CircularCloudLayouter
{
    public class PositionSearchResult
    {
        public double MinDistance { get; }
        public Segment MinSegment { get; }
        public Point ClosestRectCoordinates { get; }
        public PositionSearchResult(double minDistance, Segment minSegment, Point closestRectCoordinates)
        {
            MinDistance = minDistance;
            MinSegment = minSegment;
            ClosestRectCoordinates = closestRectCoordinates;
        }
        public PositionSearchResult Update(double minDistance, Segment minSegment, Point closestRectCoordinates)
        {
            if (minDistance < MinDistance)
            {
                return new PositionSearchResult(minDistance, minSegment, closestRectCoordinates);
            }
            return this;
        }
    }
}
