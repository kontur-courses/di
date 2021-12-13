using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudMaker
    {
        RectangleF PutRectangle(SizeF rectangleSize);
    }
}