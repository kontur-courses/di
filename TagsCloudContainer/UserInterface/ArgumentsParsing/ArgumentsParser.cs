using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.UserInterface.ArgumentsParsing
{
    public class ArgumentsParser : IArgumentsParser<UserInterfaceArguments>
    {
        public Parameters ParseArgumentsToParameters(UserInterfaceArguments arguments)
        {
            var imageSize = new Size(arguments.Width, arguments.Height);
            var fontConverter = new FontConverter();
            var font = fontConverter.ConvertFromString(arguments.Font) as Font;
            var colorConverter = new ColorConverter();
            var colors = arguments.Colors.Select(name => colorConverter.ConvertFromString(name)).Cast<Color>().ToList();
            var imageFormat = ParseImageFormat(arguments.ImageFormat);
            return new Parameters(arguments.InputFilePath, arguments.OutputFilePath, colors, font, imageSize,
                imageFormat);
        }

        private ImageFormat ParseImageFormat(string formatName)
        {
            return (ImageFormat) typeof(ImageFormat)
                .GetProperty(formatName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase)
                ?.GetValue(null);
        }
    }
}