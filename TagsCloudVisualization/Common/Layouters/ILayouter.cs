using System.Drawing;

namespace TagsCloudVisualization.Common.Layouters
{
    public interface ILayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
    }
}