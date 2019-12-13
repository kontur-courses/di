using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

namespace TagsCloudContainer
{
    public static class ArgumentsParser
    {
        public static ImageFormat ParseImageFormat(string strImageFormat)
        {
            var bindingAttr = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;
            return (ImageFormat) typeof(ImageFormat)
                .GetProperty(strImageFormat, bindingAttr)
                ?.GetValue(null);
        }

        public static Color ParseColor(string strColor)
        {
            var parsedColor = Color.FromName(strColor);
            if (!parsedColor.IsKnownColor)
                throw new ArgumentException($"Unknown color: {strColor}");
            return parsedColor;
        }

        public static Brush ParseBrush(string strBrushColor)
        {
            return new SolidBrush(ParseColor(strBrushColor));
        }
    }
}