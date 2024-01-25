namespace TagCloud.Domain.Extensions;

public static class RectangleExtensions
{
    public static RectangleF RectangleF(this Rectangle rectangle)
    {
        return new RectangleF(
            rectangle.Location,
            new SizeF(rectangle.Width + 3, rectangle.Height + 3));
    }
}