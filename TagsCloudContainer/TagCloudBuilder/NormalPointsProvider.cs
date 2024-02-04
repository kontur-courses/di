using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer.TagCloudBuilder
{
    public class NormalPointsProvider : IPointsProvider
    {
        private Random rnd = new();
        private Point Center;
        private int pointNumber = 0;
        private const int maxPointsCount = 10000000;

        public void Initialize(Point center)
        {
            Center = center;
        }

        public IEnumerable<Point> Points()
        {
            while (pointNumber < maxPointsCount)
            {
                yield return new Point((rnd.Next(0, Center.X) + rnd.Next(0, Center.X)), (rnd.Next(0, Center.Y) + rnd.Next(0, Center.Y)));
            }
            throw new ArgumentException("Reach end of placing points");
        }
    }
}