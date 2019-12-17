using System.Drawing.Imaging;
using System.Reflection;

namespace TagsCloudConsoleUI
{
    internal static class ImageFormatter
    {
        public static ImageFormat ParseImageFormat(string str)
        {
            return (ImageFormat)typeof(ImageFormat)
                .GetProperty(str, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                ?.GetValue(str, null);
        }
    }
}