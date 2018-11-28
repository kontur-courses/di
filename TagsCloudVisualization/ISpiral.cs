using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ISpiral
    {
        Rectangle GetRectangleInNextLocation(Size rectangleSize);
        Point Center { get; }
    }
}
