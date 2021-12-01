using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter
{
    public interface ICloudLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}