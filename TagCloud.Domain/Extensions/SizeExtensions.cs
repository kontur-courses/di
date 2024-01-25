namespace TagCloud.Domain.Extensions;

public static class SizeExtensions
{
    public static Size GetGreaterHalf(this Size size)
    {
        return size - size / 2;
    }
}