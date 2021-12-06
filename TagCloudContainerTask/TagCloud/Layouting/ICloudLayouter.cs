using System.Drawing;

namespace TagCloud.Layouting
{
    public interface ICloudLayouter : ILayouter
    {
        Point Center { get; }

        int GetCloudBoundaryRadius();
    }
}