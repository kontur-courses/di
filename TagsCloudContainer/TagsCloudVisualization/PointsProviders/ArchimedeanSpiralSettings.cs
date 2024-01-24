using System.Drawing;

namespace TagsCloudVisualization.PointsProviders;

public class ArchimedeanSpiralSettings
{
    public Point Center { get; init; } = new(500, 500);
    public double DeltaAngle { get; init; } = 5 * Math.PI / 180;
    public double Distance { get; init; } = 2;
}