using System.Drawing;

namespace TagsCloudVisualization.PointPlacers
{
    public interface IPointPlacer
    {
        public PointF CurrentPoint { get; }
        public void GetNextPoint();
    }
}