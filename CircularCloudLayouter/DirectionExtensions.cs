namespace CloudLayouter;

internal static class DirectionExtensions
{
    public static Direction GetReversed(this Direction direction)
    {
        return direction switch
        {
            Direction.Left => Direction.Right,
            Direction.Top => Direction.Bottom,
            Direction.Right => Direction.Left,
            Direction.Bottom => Direction.Top,
            _ => throw new ArgumentException($"Can't get reversed of {direction}", nameof(direction)),
        };
    }
}
