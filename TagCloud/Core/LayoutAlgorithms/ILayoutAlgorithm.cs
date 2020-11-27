using System.Drawing;

namespace TagCloud.Core.LayoutAlgorithms
{
    public interface ILayoutAlgorithm
    {
        public Rectangle PutNextRectangle(Size rectSize);
        public Size GetLayoutSize();
    }
}