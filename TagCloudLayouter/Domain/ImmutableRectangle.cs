namespace CircularCloudLayouter.Domain;

public readonly struct ImmutableRectangle
{
    public int X { get; }
    public int Y { get; }
    public int Width { get; }
    public int Height { get; }

    public ImmutablePoint Location => new(X, Y);
    public ImmutableSize Size => new(Width, Height);
    
    public int Left => X;
    public int Top => Y;
    public int Right => unchecked(X + Width);
    public int Bottom => unchecked(Y + Height);

    public ImmutableRectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public ImmutableRectangle(int x, int y, ImmutableSize size) : this(x, y, size.Width, size.Height)
    {
    }

    public ImmutableRectangle(ImmutablePoint location, int width, int height) : this(location.X, location.Y, width, height)
    {
    }

    public ImmutableRectangle(ImmutablePoint location, ImmutableSize size) : this(location.X, location.Y, size.Width, size.Height)
    {
    }
}