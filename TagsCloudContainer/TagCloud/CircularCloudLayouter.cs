using System.Drawing;
using TagsCloudContainer.UI;
using TagsCloudContainer.utility;

namespace TagsCloudContainer.TagCloud;

public class CircularCloudLayouter(ApplicationArguments args) : ICircularCloudLayouter
{
    private int radius;
    private int minDimension = int.MaxValue;
    private readonly List<Rectangle> placedRectangles = [];

    public IEnumerable<Rectangle> PlacedRectangles => placedRectangles.AsReadOnly();

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        minDimension = Math.Min(minDimension, rectangleSize.Height);

        foreach (var coordinate in GetPoints())
        {
            var target = new Rectangle(
                new Point(
                    coordinate.X - rectangleSize.Width / 2,
                    coordinate.Y - rectangleSize.Height / 2
                ), rectangleSize);

            if (!IntersectWithPlaced(target))
            {
                CompactRectangle(ref target);

                placedRectangles.Add(target);

                return target;
            }
        }

        return default;
    }

    private void CompactRectangle(ref Rectangle target)
    {
        var movableX = true;
        var movableY = true;
        while ((movableX || movableY) && !IntersectWithPlaced(target))
        {
            if (movableX)
            {
                if (args.Center[0] == target.X + target.Width / 2)
                {
                    movableX = false;
                    continue;
                }

                target.X += Math.Sign(args.Center[0] - (target.X + target.Width / 2));

                if (IntersectWithPlaced(target))
                {
                    target.X -= Math.Sign(args.Center[0] - (target.X + target.Width / 2));
                    movableX = false;
                }
            }

            if (movableY)
            {
                if (args.Center[1] == target.Y + target.Height / 2)
                {
                    movableY = false;
                    continue;
                }

                target.Y += Math.Sign(args.Center[1] - (target.Y + target.Height / 2));

                if (IntersectWithPlaced(target))
                {
                    target.Y -= Math.Sign(args.Center[1] - (target.Y + target.Height / 2));
                    movableY = false;
                }
            }
        }
    }

    private IEnumerable<Point> GetPoints()
    {
        var rnd = new Random();
        while (true)
        {
            var angle = rnd.Next(360);
            for (var i = 0; i < 360; angle++, i++)
                yield return PointMath.PolarToCartesian(radius, angle, args.Center[0], args.Center[1]);
            radius += minDimension;
        }
    }

    private bool IntersectWithPlaced(Rectangle target)
    {
        return placedRectangles.Any(target.IntersectsWith);
    }
}