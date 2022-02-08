using SixLabors.ImageSharp;

namespace TagsCloudContainer.Render.CircularCloud;

public class CircularCloudRenderOptions : CloudRenderOptions
{
    public Point ImageCenter => new(ImageWidth / 2, ImageHeight / 2);
    public double CurveStartingRadius { get; set; } = 0;
    public double MinimumCurveAngleStep { get; set; } = 0.2;
    public double CurveAngleStepSlowDown { get; set; } = 0.002;
    public double DirectionBetweenRoundsCoefficient { get; set; } = 1 / (2 * Math.PI);
    public double CurveAngleStep { get; set; } = Math.PI / 10;
}