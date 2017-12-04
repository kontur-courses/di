using System.Drawing;

namespace TagCloud
{
    public interface ICloudLayouter
    {
        void PutNextRectangle(Size rectangleSize, string text);
        TextRectangle[] CloudRectangles { get; }
    }
}