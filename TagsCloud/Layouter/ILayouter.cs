using System.Drawing;

namespace TagsCloud.Layouter
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        Size GetLayoutSize();
    }
}
