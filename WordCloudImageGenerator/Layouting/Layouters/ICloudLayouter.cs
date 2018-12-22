using System.Drawing;

namespace WordCloudImageGenerator.LayoutCraetion.Layouters
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}