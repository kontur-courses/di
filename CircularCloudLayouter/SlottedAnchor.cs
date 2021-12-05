using System.Drawing;

namespace CloudLayouter;

internal class SlottedAnchor
{
    public readonly Rectangle Rectangle;
    public Direction FilledSlots { get; private set; }

    public SlottedAnchor(Rectangle rectangle, Direction filledSlots)
    {
        Rectangle = rectangle;
        FilledSlots = filledSlots;
    }

    public int Left => Rectangle.Left;
    public int Right => Rectangle.Right;
    public int Top => Rectangle.Top;
    public int Bottom => Rectangle.Bottom;
    public int Width => Rectangle.Width;
    public int Height => Rectangle.Height;

    public void FillDirection(Direction direction)
    {
        FilledSlots |= direction;
    }

    public bool HaveEmptySlot(Direction direction)
    {
        return (direction & FilledSlots) == Direction.None;
    }

    public bool IntersectsWith(Rectangle rect)
    {
        return Rectangle.IntersectsWith(rect);
    }

    public bool IntersectsWith(SlottedAnchor anchor)
    {
        return Rectangle.IntersectsWith(anchor.Rectangle);
    }

    public Point GetCenter()
    {
        return Rectangle.GetCenter();
    }

    public IEnumerable<Direction> GetEmptySlots()
    {
        if (HaveEmptySlot(Direction.Top))
            yield return Direction.Top;
        if (HaveEmptySlot(Direction.Bottom))
            yield return Direction.Bottom;
        if (HaveEmptySlot(Direction.Right))
            yield return Direction.Right;
        if (HaveEmptySlot(Direction.Left))
            yield return Direction.Left;
    }
}
