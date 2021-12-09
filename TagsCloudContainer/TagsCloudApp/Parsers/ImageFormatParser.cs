using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using TagsCloudContainer;

namespace TagsCloudApp.Parsers
{
    public class ImageFormatParser : IImageFormatParser
    {
        private const BindingFlags bindingAttributes =
            BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;

        public ImageFormat Parse(string value)
        {
            var formatResult = TryGetImageFormat(value);
            if (formatResult.Success)
                return formatResult.Value;

            throw GenerateException();
        }

        private static Result<ImageFormat> TryGetImageFormat(string value)
        {
            var format = (ImageFormat?)typeof(ImageFormat)
                .GetProperty(value, bindingAttributes)
                ?.GetValue(null);

            return format != null
                ? new Result<ImageFormat>(format)
                : new Result<ImageFormat>(new Exception("Can't find image format."));
        }

        private static ApplicationException GenerateException()
        {
            var availableFormats = string.Join(
                ", ",
                typeof(ImageFormat).GetProperties(bindingAttributes).Select(propInfo => propInfo.Name));

            return new ApplicationException("Available formats: " + availableFormats);
        }
    }
}