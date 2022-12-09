using System.Drawing;

namespace TagsCloudVisualization;

public interface ILayouter
{
    public Rectangle PutNextRectangle(Size size);
}