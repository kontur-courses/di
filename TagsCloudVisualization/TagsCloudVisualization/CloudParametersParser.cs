using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CloudParametersParser : ICloudParametersParser
    {
        public CloudParameters Parse(Options options)
        {
            var parameters = new CloudParameters
            {
                ColorFunc = GetColor(options.Color),
                ImageSize = GetImageSize(options.ImageSize),
                FontName = options.FontName,
                OutFormat = GetImageFormat(options.OutFormat)
            };

            return parameters;
        }

        private ImageFormat GetImageFormat(string input)
        {
            switch (input)
            {
                case "tiff":
                    return ImageFormat.Tiff;
                case "png":
                    return ImageFormat.Png;
                default:
                case "jpeg":
                    return ImageFormat.Jpeg;
            }
        }

        private static Func<float, Color> GetColor(string input)
        {
            var rnd = new Random();
            switch (input)
            {
                case "random":
                    return x => Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                case "rainbow":
                    return GetRainbowColor;
                default:
                    return x => Color.FromName(input);
            }
        }

        private Size GetImageSize(string input)
        {
            var delimiter = input.IndexOf('x');
            int.TryParse(input.Substring(0, delimiter), out var width);
            int.TryParse(input.Substring(delimiter + 1), out var height);
            return new Size(width, height);
        }

        public static Color GetRainbowColor(float progress)
        {
            var div = Math.Abs(progress % 1) * 6;
            var ascending = (int)(div % 1 * 255);
            var descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }
    }
}
