using System.Collections.Generic;
using CommandLine;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class SimpleCommandLineParser : ICommandLineParser<SimpleConfiguration>
    {
        public SimpleConfiguration Parse(IEnumerable<string> args)
        {
            var configuration = new SimpleConfiguration();
            Parser.Default.ParseArguments<SimpleConfiguration>(args)
                .WithParsed(parsed =>
                {
                    configuration.PathToWordsFile = parsed.PathToWordsFile;
                    configuration.DirectoryToSave = parsed.DirectoryToSave;
                    configuration.OutFileName = parsed.OutFileName;
                    configuration.FontFamily = parsed.FontFamily;
                    configuration.Color = parsed.Color;
                    configuration.FontSize = parsed.FontSize;
                    configuration.ImageWidth = parsed.ImageWidth;
                    configuration.ImageHeight = parsed.ImageHeight;
                    configuration.RotationAngle = parsed.RotationAngle;
                });

            return configuration;
        }
    }


    public class SimpleConfiguration : IConfiguration
    {
        [Value(0, Required = true,
            HelpText = "File with words for Cloud")]
        public string PathToWordsFile { get; set; }

        [Value(1, Required = true,
            HelpText ="Directory to save")]
        public string DirectoryToSave { get; set; }

        [Value(2, Required = true,
            HelpText = "Output image name")]
        public string OutFileName { get; set; }

        [Option('f', "font", Default = "Arial",
            HelpText = "Font (e.g. Arial)")]
        public string FontFamily { get; set; }

        [Option('c', "color", Default = "Black",
            HelpText = "Font color")]
        public string Color { get; set; }

        [Option('s', "fontSize", Default = 32,
            HelpText = "Font size")]
        public int FontSize { get; set; }

        [Option('w', "imageWidth", Default = 2048,
            HelpText = "Image width")]
        public int ImageWidth { get; set; }

        [Option('h', "imageHeight", Default = 1024,
            HelpText = "Image height")]
        public int ImageHeight { get; set; }

        [Option('a', "angle", Default = 1,
            HelpText = "Rotation angle step of Circular Cloud")]
        public int RotationAngle { get; set; }
    }
}