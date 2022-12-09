namespace CircularCloudLayouter.Domain;

public readonly record struct ImmutablePoint(int X, int Y)
{
    public static ImmutablePoint operator +(ImmutablePoint first, ImmutablePoint second) =>
        new(first.X + second.X, first.Y + second.Y);

    public static ImmutablePoint operator -(ImmutablePoint first, ImmutablePoint second) =>
        new(first.X - second.X, first.Y - second.Y);
}