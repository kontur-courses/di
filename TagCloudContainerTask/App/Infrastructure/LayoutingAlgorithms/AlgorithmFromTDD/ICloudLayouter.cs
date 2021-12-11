using System.Drawing;

namespace App.Infrastructure.LayoutingAlgorithms.AlgorithmFromTDD
{
    public interface ICloudLayouter : ILayouter
    {
        Point Center { get; }

        int GetCloudBoundaryRadius();
    }
}