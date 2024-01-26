using Aspose.Drawing;

namespace TagCloud.Utils.Extensions;

public static class TupleExtensions
{
    public static bool TryParseColor(this (int red, int green, int blue) from, out Color color)
    {
        try
        {
            color = Color.FromArgb(255, from.red, from.green, from.blue);
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Color scheme must be in range [0, 255]");
        }
    }
}