using System.Drawing;

namespace TagCloud.LayoutAlgorithms.AlgorithmFromTDD.Layouting
{
    public interface ICloudLayouter : ILayouter
    {
        Point Center { get; }

        int GetCloudBoundaryRadius();
    }
}