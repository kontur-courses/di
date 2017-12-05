using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface ICloudLayouter
    {
        void PutNextRectangle(Size rectangleSize, string text);
        TextRectangle[] CloudRectangles { get; }
    }
}