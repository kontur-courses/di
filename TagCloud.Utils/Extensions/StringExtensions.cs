using System.Reflection;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace TagCloud.Utils.Extensions;

public static class StringExtensions
{
    public static bool TryConvertToImageFormat(this string str, out ImageFormat format)
    {
        format = typeof(ImageFormat)
            .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
            ?.GetValue(null) as ImageFormat;

        return format is not null;
    }

    public static bool TryParseFontFamily(this string str, out FontFamily fontFamily)
    {
        try
        {
            fontFamily = new FontFamily(str);
            return true;
        }
        catch (Exception e)
        {
            fontFamily = default;
            return false;
        }
    }
}