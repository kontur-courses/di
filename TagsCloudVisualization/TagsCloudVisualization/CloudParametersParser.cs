using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class CloudParametersParser : ICloudParametersParser
    {
        public CloudParameters Parse(Options options, CloudParameters parameters)
        {
            parameters.Colors = GetColors(options.Color);
            parameters.ImageSize = GetImageSize(options.ImageSize);
            parameters.FontName = options.FontName;
            parameters.OutFormat = GetImageFormat(options.OutFormat);

            return parameters;
        }

        private ImageFormat GetImageFormat(string input)
        {
            switch (input)
            {
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "tiff":
                    return ImageFormat.Tiff;
                case "png":
                    return ImageFormat.Png;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        private static IEnumerable<Color> GetColors(string input)
        {
            var rnd = new Random();
            switch (input)
            {
                case "random":
                    return new[] {Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))};
                case "rainbow":
                    return new[]
                    {
                        Color.Red,
                        Color.Orange,
                        Color.Yellow,
                        Color.Green,
                        Color.DeepSkyBlue,
                        Color.Blue,
                        Color.BlueViolet
                    };
                default:
                    return new[] {Color.FromName(input)};
            }
        }

        private Size GetImageSize(string input)
        {
            var delimiter = input.IndexOf('x');
            int.TryParse(input.Substring(0, delimiter), out var width);
            int.TryParse(input.Substring(delimiter + 1), out var height);
            return new Size(width, height);
        }
    }
}