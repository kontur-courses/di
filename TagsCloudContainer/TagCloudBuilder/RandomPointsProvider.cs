using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer.TagCloudBuilder
{
    public class RandomPointsProvider : IPointsProvider
    {
        private Random rnd = new Random();
        private Point Center;
        private int pointNumber = 0;
        private const int maxPonitsCount = 10000000;

        public IEnumerable<Point> Points()
        {
            while (pointNumber < maxPonitsCount)
            {
                yield return new Point(rnd.Next(0, Center.X * 2), rnd.Next(0, Center.Y * 2));
            }
            throw new ArgumentException("Reach end of placing points");
        }

        public void Initialize(Point center)
        {
            Center = center;
        }
    }
}