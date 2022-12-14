using System.Drawing;

namespace TagCloud;

public static class SizeExtensions
{
    public static bool IsPositive(this Size size)
    {
        return size is { Width: > 0, Height: > 0 };
    }

    public static int Area(this Size size)
    {
        return size.Width * size.Height;
    }
}