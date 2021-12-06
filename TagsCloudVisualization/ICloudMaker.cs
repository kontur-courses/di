using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudMaker
    {
        RectangleF PutRectangle(Size rectangleSize);
    }
}