using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}