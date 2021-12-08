using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;

namespace TagsCloudApp
{
    public class ImageFormatParser : IObjectParser<ImageFormat>
    {
        public ImageFormat Parse(string value)
        {
            var bindingAttributes = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;
            var format = (ImageFormat?)typeof(ImageFormat)
                .GetProperty(value, bindingAttributes)
                ?.GetValue(null);

            if (format != null)
                return format;

            var availableFormats = string.Join(", ",
                typeof(ImageFormat).GetProperties(bindingAttributes).Select(propInfo => propInfo.Name));

            throw new ApplicationException("Available formats: " + availableFormats);
        }
    }
}