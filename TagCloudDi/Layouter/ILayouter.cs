using System.Drawing;

namespace TagCloudDi.Layouter
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}
