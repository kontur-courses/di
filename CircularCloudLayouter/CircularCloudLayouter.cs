using CloudLayouter.Internals;
using System.Drawing;

namespace CloudLayouter;

public class CircularCloudLayouter
{
    public Point Center { get; private set; }

    private readonly List<SlottedAnchor> anchors = new();
    public CircularCloudLayouter(Point center)
    {
        Center = center;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width == 0 || rectangleSize.Height == 0)
            throw new ArgumentOutOfRangeException(nameof(rectangleSize));
        if (rectangleSize.Width < 0 || rectangleSize.Height < 0)
            rectangleSize = rectangleSize.Abs();

        SlottedAnchor anchor;
        if (anchors.Count == 0)
        {
            var location = Center + rectangleSize / 2 * -1;
            var rectangle = new Rectangle(location, rectangleSize);
            anchor = new(rectangle, Direction.None);
        }
        else
        {
            anchor = CreateNextAnchor(rectangleSize);
        }

        anchors.Add(anchor);

        return anchor.Rectangle;
    }

    private SlottedAnchor CreateNextAnchor(Size nextSize)
    {
        var filteredAnchors = anchors.FilterFilledSlots();
        var allSlots = filteredAnchors.SelectMany(anchor => anchor.GetAllValidSlots().Select(slot => (parent: anchor, slot)));
        var orderedSlots = allSlots.OrderBy(x => x.slot.point.DistanceTo(Center));
        var allRectangles = orderedSlots.Select(x =>
        {
            var slot = (rect: CreateRectangleAt(x.slot.point, x.slot.direction, nextSize), x.slot.direction);
            return (x.parent, slot);
        });

        var (parent, current) = allRectangles.First(x => !x.slot.rect.IntersectsWithAny(anchors));

        parent.FillDirection(current.direction);
        return new(current.rect, current.direction.GetReversed());
    }

    private static Rectangle CreateRectangleAt(Point slot, Direction direction, Size size)
    {
        var location = CircularCloudLayouterInternals.GetLocationForSlotAt(slot, direction, size);
        return new(location, size);
    }
}
