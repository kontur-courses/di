using System.Drawing;

namespace TagsCloudContainer.Layouter.PointsProviders
{
    public interface IPointsProvider
    {
        public LayoutAlrogorithm AlghorithmName { get; }
        public Point GetNextPoint();
    }
}