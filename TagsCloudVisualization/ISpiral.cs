using System.Drawing;

namespace TagsCloudVisualization
{
    interface ISpiral
    {
        Rectangle GetRectangleInNextLocation(Size rectangleSize);
    }
}
