using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        Point Center { get; }

        int Radius { get; }

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}