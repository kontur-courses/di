using System.Drawing;
using TagsCloud.Core.Helpers;

namespace TagsCloud.Core.Layouters.Spirals;

public class ArchimedeanSpiral : ISpiral
{
    private readonly Point center;

    public ArchimedeanSpiral(Point center, double spiralStepInPixels, double angleStepInDegrees)
    {
        if (spiralStepInPixels == 0)
            throw new ArgumentException("Spiral step should not be equals zero");
        if (angleStepInDegrees == 0)
            throw new ArgumentException("Angle step should not be equals zero");

        this.center = center;
        SpiralStepInPixels = spiralStepInPixels;
        AngleStepInRadians = angleStepInDegrees * Math.PI / 180;
        CurrentAngleInRadians = 0;
    }

    public double SpiralStepInPixels { get; }

    public double AngleStepInRadians { get; }

    public double CurrentAngleInRadians { get; private set; }

    public Point GetNextPoint()
    {
        var polarRadius = SpiralStepInPixels / (Math.PI * 2) * CurrentAngleInRadians;
        var x = polarRadius * Math.Cos(CurrentAngleInRadians);
        var y = polarRadius * Math.Sin(CurrentAngleInRadians);

        var localPosition = new Point(Convert.ToInt32(x), Convert.ToInt32(y));

        CurrentAngleInRadians += AngleStepInRadians;

        return localPosition.Plus(center);
    }
}