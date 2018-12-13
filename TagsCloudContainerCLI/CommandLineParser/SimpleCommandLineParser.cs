using System.Collections.Generic;
using CommandLine;
using TagsCloudContainer.Configuration;

namespace TagsCloudContainerCLI.CommandLineParser
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
                    configuration.BoringWordsFileName = parsed.BoringWordsFileName;
                    configuration.DirectoryToSave = parsed.DirectoryToSave;
                    configuration.OutFileName = parsed.OutFileName;
                    configuration.FontFamily = parsed.FontFamily;
                    configuration.Color = parsed.Color;
                    configuration.MinFontSize = parsed.MinFontSize;
                    configuration.MaxFontSize = parsed.MaxFontSize;
                    configuration.ImageWidth = parsed.ImageWidth;
                    configuration.ImageHeight = parsed.ImageHeight;
                    configuration.RotationAngle = parsed.RotationAngle;
                    configuration.CenterX = parsed.CenterX;
                    configuration.CenterY = parsed.CenterY;
                });

            return configuration;
        }
    }
}