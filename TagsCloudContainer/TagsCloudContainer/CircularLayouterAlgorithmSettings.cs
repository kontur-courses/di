using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class CircularLayouterAlgorithmSettings : LayouterAlgorithmSettings
{
    public Point Center { get; set; } = new(500, 500);
    public float PolarStepK { get; set; } = 0.05f;
    public float AngleStep { get; set; } = .01f;
    public float StartAngle { get; set; } = 0f;
}