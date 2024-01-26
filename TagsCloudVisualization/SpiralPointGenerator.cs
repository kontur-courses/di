using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public class SpiralPointGenerator : IPointGenerator
{
    private readonly float distanceDelta, angleDelta;
    private float currentAngle;

    public SpiralPointGenerator(float distanceDelta, float angleDelta)
    {
        this.distanceDelta = distanceDelta;
        this.angleDelta = angleDelta;
    }

    public PointF GetNextPoint()
    {
        var radius = distanceDelta * currentAngle;
        var point = new PointF(radius, currentAngle);

        currentAngle += angleDelta;
        point = point.ConvertToCartesian();

        return point;
    }
}