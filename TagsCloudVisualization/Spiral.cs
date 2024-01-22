using SixLabors.ImageSharp;

namespace TagsCloudVisualization;

public class Spiral : ILayoutFunction
{
    private readonly float distanceDelta, angleDelta;
    private float currentAngle;

    public Spiral(float distanceDelta, float angleDelta)
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