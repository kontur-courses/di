using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer.TagCloudBuilder
{
    public class RandomPointsProvider : IPointsProvider
    {
        private Random rnd = new Random();
        private readonly Point Center;
        private int pointNumber = 0;

        public RandomPointsProvider(Point center)
        {
            Center = center;
        }

        public IEnumerable<Point> Points()
        {
            while (pointNumber < 10000000) // Limit number of returned points for safety reason
            {
                yield return new Point(rnd.Next(0, Center.X * 2), rnd.Next(0, Center.Y * 2));
            }
            throw new ArgumentException("Reach end of placing points");
        }

        public void Reset()
        {
            rnd = new Random();
            pointNumber = 0;
        }
    }
}
