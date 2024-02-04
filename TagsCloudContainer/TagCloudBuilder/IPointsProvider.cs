using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IPointsProvider
    {
        public IEnumerable<Point> Points();
        public void Initialize(Point center);
    }
}