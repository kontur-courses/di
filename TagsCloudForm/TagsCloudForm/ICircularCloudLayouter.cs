using System.Drawing;

namespace TagsCloudVisualization
{
    interface ICircularCloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
