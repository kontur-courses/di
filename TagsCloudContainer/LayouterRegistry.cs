using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;

public static class LayouterRegistry
{
    public static ICloudLayouter GetLayout(LayouterType layouterType)
    {
        switch (layouterType)
        {
            case LayouterType.Block:
                return new BlockCloudLayouter(new Point(0, 0));
            case LayouterType.Spiral:
                return new SpiralCloudLayouter(new Point(0, 0));
            default:
                return new SpiralCloudLayouter(new Point(0, 0));
        }
    }
}