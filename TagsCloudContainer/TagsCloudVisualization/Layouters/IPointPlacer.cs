using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public interface IPointPlacer
    {
        public PointF CurrentPoint { get; }
        public void GetNextPoint();
    }
}