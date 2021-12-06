using System.Drawing;

namespace TagsCloudContainer.Layouter.PointsCreators
{
    public interface IPointsProvider
    {
        public Point GetNextPoint();
    }
}