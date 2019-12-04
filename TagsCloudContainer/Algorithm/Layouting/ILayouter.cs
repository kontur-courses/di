using System.Drawing;

namespace TagsCloudContainer.Algorithm.Layouting
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}