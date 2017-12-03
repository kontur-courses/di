using System.Drawing;

namespace TagCloudMaker
{
    public interface ICloudLayouter
    {
        void PutNextRectangle(Size rectangleSize, string text);
        TextRectangle[] Cloud { get; }
    }
}