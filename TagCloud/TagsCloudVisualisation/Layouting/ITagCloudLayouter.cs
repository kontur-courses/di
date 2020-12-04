using System.Drawing;

namespace TagsCloudVisualisation.Layouting
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}