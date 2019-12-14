using System.Drawing;

namespace TagsCloudVisualization.TagCloudLayouter
{
    public interface ITagCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}