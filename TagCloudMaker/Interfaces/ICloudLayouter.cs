using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface ICloudLayouter
    {
        Result<None> PutNextRectangle(Size rectangleSize, string text);
        TextRectangle[] CloudRectangles { get; }
    }
}