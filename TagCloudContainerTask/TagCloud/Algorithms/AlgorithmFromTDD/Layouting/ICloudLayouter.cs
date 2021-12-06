using System.Drawing;

namespace TagCloud.Algorithms.AlgorithmFromTDD.Layouting
{
    public interface ICloudLayouter : ILayouter
    {
        Point Center { get; }

        int GetCloudBoundaryRadius();
    }
}