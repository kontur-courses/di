using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IPointsProvider
    {
        public IEnumerable<Point> Points();
        public void Reset();
    }
}
