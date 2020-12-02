using System.Drawing;

namespace TagsCloud.LayoutAlgorithms
{
    interface ILayoutAlgorithm
    {
        public Rectangle PutNextRectangle(Size size);
        public Size GetSize();
    }
}
