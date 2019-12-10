using System.Drawing;

namespace TagsCloudVisualization.Services
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        void Reset();
    }
}