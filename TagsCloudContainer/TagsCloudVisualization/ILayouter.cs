using System.Drawing;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}