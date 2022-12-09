namespace CircularCloudLayouter.Domain;

public readonly struct ImmutableSize
{
    public int Width { get; }

    public int Height { get; }

    public ImmutableSize(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static ImmutableSize operator *(ImmutableSize size, int value) =>
        new(size.Width * 2, size.Height * 2);

    public static ImmutableSize operator /(ImmutableSize size, int value) =>
        new(size.Width / 2, size.Height / 2);

    public static implicit operator ImmutablePoint(ImmutableSize size) =>
        new(size.Width, size.Height);
}