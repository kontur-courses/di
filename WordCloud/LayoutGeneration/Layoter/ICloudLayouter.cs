using System.Drawing;

namespace WordCloud.LayoutGeneration.Layoter
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size rectangleSize);
        void Reset();
    }
}