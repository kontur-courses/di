using System.Drawing;

namespace TagCloud.LayoutAlgorithm.CircularLayoutAlgorithm;

public class FermatSpiral
{
    private readonly int angleStep;
    private readonly Point center;
    private int currentAngle;
    private int spiralRadiusCoefficient;

    public FermatSpiral(Point center, int spiralRadiusCoefficient = 1, int angleStep = 1)
    {
        this.spiralRadiusCoefficient = spiralRadiusCoefficient;
        this.angleStep = angleStep;
        this.center = center;
        currentAngle = 0;
    }

    public Point GetNextPoint()
    {
        if (currentAngle == 0)
        {
            currentAngle += angleStep;
            return center;
        }
        
        var result = new Point((int)(spiralRadiusCoefficient * Math.Sqrt(currentAngle) * Math.Cos(currentAngle) + center.X),
            (int)(spiralRadiusCoefficient * Math.Sqrt(currentAngle) * Math.Sin(currentAngle)) + center.Y);

        if (spiralRadiusCoefficient < 0)
            currentAngle += angleStep;
        spiralRadiusCoefficient = -spiralRadiusCoefficient;
        return result;
    }
}