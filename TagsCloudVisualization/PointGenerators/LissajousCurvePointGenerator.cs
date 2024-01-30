using System.Drawing;

namespace TagsCloudVisualization;

public class LissajousCurvePointGenerator : IPointGenerator
{
    private int xAmplitude = 100;
    private int yAmplitude = 100;
    private int xConstant = 19;
    private int yConstant = 20;
    private double delta = Math.PI / 2;
    private double parameter = 0;

    public Algorithm Name { get; } = Algorithm.Square;

    public Point GetNextPoint()
    {
        parameter += 0.01;
        var x = Math.Round(xAmplitude * Math.Sin(xConstant * parameter + delta));
        var y = Math.Round(yAmplitude * Math.Sin(yConstant * parameter));

        if (parameter > 20)
        {
            parameter = 0;
            xAmplitude += 20;
            yAmplitude += 20;
        }

        return new Point((int)x, (int)y);
    }
}