using System.Drawing;

namespace TagCloud.Core.LayoutAlgorithms
{
    public interface ISpiral
    {
        public Point Start { get; }
        public PointF GetNextPoint();
    }
}