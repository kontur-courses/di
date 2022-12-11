using System.Drawing;

namespace TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;

public class SpiralCloudLayouterSettings : ICloudLayouterSettings
{
    public Point Center { get; set; }
    public double SpiralStep { get; set; }
    public double RotationStep { get; set; }
        
    public SpiralCloudLayouterSettings(Point center, double spiralStep, double rotationStep)
    {
        Center = center;
        SpiralStep = spiralStep;
        RotationStep = rotationStep;
    }
}