using System.Drawing;

namespace TagCloudMaker
{
    public interface ICloudLayouter
    {
        TextRectangle PutNextRectangle(Size rectangleSize, string text);
    }
}