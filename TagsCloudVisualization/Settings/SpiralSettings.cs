namespace TagsCloudVisualization.Settings;

public class SpiralSettings
{
    public double DeltaAngle { get; }
    public double DeltaRadius { get; }

    public SpiralSettings(double deltaAngle, double deltaRadius) 
    {
        DeltaAngle = deltaAngle;
        DeltaRadius = deltaRadius;
    }
}
