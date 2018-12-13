using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CommandLine;
using TagsCloudContainer.Configuration;

namespace TagsCloudContainerCLI.CommandLineParser
{
    public class SimpleCommandLineParser : ICommandLineParser<SimpleConfiguration>
    {
        public SimpleConfiguration Parse(IEnumerable<string> args)
        {
            var configuration = new SimpleConfiguration();
            Parser.Default.ParseArguments<ParseTemplate>(args)
                .WithParsed(parsed =>
                {
                    configuration.PathToWordsFile = parsed.PathToWordsFile;
                    configuration.BoringWordsFileName = parsed.BoringWordsFileName;
                    configuration.DirectoryToSave = parsed.DirectoryToSave;
                    configuration.OutFileName = parsed.OutFileName;
                    configuration.FontFamily = CommandLineArgumentConverter.ConvertToFontFamily(parsed.FontFamily);
                    configuration.Color = CommandLineArgumentConverter.ConvertToColor(parsed.Color);
                    configuration.MinFontSize = parsed.MinFontSize;
                    configuration.MaxFontSize = parsed.MaxFontSize;
                    configuration.ImageWidth = parsed.ImageWidth;
                    configuration.ImageHeight = parsed.ImageHeight;
                    configuration.ImageFormat = CommandLineArgumentConverter.ConvertToImageFormat(parsed.ImageFormat);
                    configuration.RotationAngle = parsed.RotationAngle;
                    configuration.CenterX = parsed.CenterX;
                    configuration.CenterY = parsed.CenterY;
                });

            return configuration;
        }

        private static class CommandLineArgumentConverter
        {
            public static FontFamily ConvertToFontFamily(string arg) => new FontFamily(arg);

            public static Color ConvertToColor(string arg) => Color.FromName(arg);

            public static ImageFormat ConvertToImageFormat(string arg)
            {
                ImageFormat format = null;

                var prop = typeof(ImageFormat).GetProperties()
                    .FirstOrDefault(p => p.Name.Equals(arg, StringComparison.InvariantCultureIgnoreCase));

                if (prop != null)
                    format = prop.GetValue(prop) as ImageFormat;

                return format;
            }
        }

        private class ParseTemplate
        {
            [Value(0, Required = true,
                HelpText = "File with Cloud tags")]
            public string PathToWordsFile { get; set; }

            [Value(1, Required = true,
                HelpText = "File with words to exclude")]
            public string BoringWordsFileName { get; set; }

            [Value(2, Required = true,
                HelpText ="Directory to save TagCloud image")]
            public string DirectoryToSave { get; set; }

            [Value(3, Required = true,
                HelpText = "Output image name")]
            public string OutFileName { get; set; }

            [Option('f', "font", Default = "Calibri Light",
                HelpText = "Tags font (e.g. Arial, Verdana, Comic Sans MS)")]
            public string FontFamily { get; set; }

            [Option('c', "color", Default = "Black",
                HelpText = "Tags font color")]
            public string Color { get; set; }

            [Option("minFontSize", Default = 24,
                HelpText = "Minimum font size")]
            public int MinFontSize { get; set; }

            [Option("maxFontSize", Default = 256,
                HelpText = "Maximum font size")]
            public int MaxFontSize { get; set; }

            [Option('w', "imageWidth", Default = 2048,
                HelpText = "Image width")]
            public int ImageWidth { get; set; }

            [Option('h', "imageHeight", Default = 1024,
                HelpText = "Image height")]
            public int ImageHeight { get; set; }

            [Option("imageFormat", Default = "jpeg",
                HelpText = "Image format")]
            public string ImageFormat { get; set; }

            [Option('a', "angle", Default = 1,
                HelpText = "Rotation angle step of Circular Cloud")]
            public int RotationAngle { get; set; }

            [Option('x', "centerX", Default = 0,
                HelpText = "Center of Cloud by abscissa")]
            public int CenterX { get; set; }

            [Option('y', "centerY", Default = 0,
                HelpText = "Center of Cloud by ordinate")]
            public int CenterY { get; set; }
        }
    }
}