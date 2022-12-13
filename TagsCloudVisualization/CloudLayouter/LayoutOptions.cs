using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter;

public class LayoutOptions
{
    public PointF Center { get; }
    public float SpiralStep { get; }

    public LayoutOptions(PointF center, float spiralStep)
    {
        Center = center;
        SpiralStep = spiralStep;
    }
}