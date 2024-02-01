using System.Drawing;
using System.Numerics;

public class TagCloudLayouter : ITagCloudLayouter
{
    private int rectanglesPlaced;
    private List<RectangleF> rectangles = new List<RectangleF>();
    private readonly PointF center;
    private RectangleF currentRectangle;
    private int segmentsCount = 4;
    private List<Vector2> directions = new List<Vector2>();

    public TagCloudLayouter(TagCloudOptions options)
    {
        center = options.Center;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
            throw new ArgumentException("Rectangle can't be negative size.");

        currentRectangle = new RectangleF(center, rectangleSize);

        MoveOutOfCenterInDirection(ref currentRectangle, GetDirection());

        MoveTowardsCenter(ref currentRectangle);

        rectangles.Add(currentRectangle);

        return Rectangle.Truncate(currentRectangle);
    }

    private void MoveOutOfCenterInDirection(ref RectangleF rect, Vector2 direction)
    {
        while (IntersectsWithAny(rect))
            rect.Offset(direction.X, direction.Y);
    }

    private void MoveTowardsCenter(ref RectangleF rect)
    {
        bool movedDx = true, movedDy = true;
        while (movedDx || movedDy)
        {
            var dx = (int)(center.X - rect.X);
            var dy = (int)(center.Y - rect.Y);
            dx /= dx != 0 ? Math.Abs(dx) : 1;
            dy /= dy != 0 ? Math.Abs(dy) : 1;

            movedDx = dx != 0 && OffsetIfDontCollide(ref rect, dx, 0);
            movedDy = dy != 0 && OffsetIfDontCollide(ref rect, 0, dy);
        }
    }

    private bool OffsetIfDontCollide(ref RectangleF rect, float x, float y)
    {
        rect.Offset(x, y);

        if (IntersectsWithAny(rect))
        {
            rect.Offset(-x, -y);
            return false;
        }

        return true;
    }

    private bool IntersectsWithAny(RectangleF rect)
    {
        rect.X = (int)rect.X;
        rect.Y = (int)rect.Y;
        return rectangles.Any(r => rect.IntersectsWith(r));
    }

    private Vector2 GetDirection()
    {
        if (rectanglesPlaced % 100 == 0)
        {
            segmentsCount++;
            UpdateDirections();
        }

        return directions[rectanglesPlaced++ % directions.Count];
    }

    private void UpdateDirections()
    {
        var directions = new List<Vector2>();

        var step = Math.PI / 2 / segmentsCount;

        var multipliers = new[] { -1, 1, 1, -1, -1 };

        for (var angle = step; angle <= Math.PI / 2 - step; angle += step)
        {
            // координаты на первой четверти единичной окружности
            var x = (float)Math.Cos(angle);
            var y = (float)Math.Sin(angle);

            // добавление координат соответствующих точек на всех четвертях окружности
            for (var j = 0; j < multipliers.Length - 1; j++)
            {
                var vector = new Vector2(x * multipliers[j], y * multipliers[j + 1]);
                vector /= vector.Length();
                directions.Add(vector);
            }
        }

        this.directions = directions;
    }
}