using System.Drawing;

namespace TagCloud;

public static class SizeExtensions
{
    public static bool IsPositive(this Size size)
    {
        return size.Width > 0 && size.Height > 0;
    }
}