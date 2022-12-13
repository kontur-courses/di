using CommandLine;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.CommandLineParsing;
using TagCloud.ImageProcessing;
using TagCloud.WordColoring;

namespace TagCloud.AppConfig
{
    public class ConsoleAppConfigProvider : IAppConfigProvider
    {
        private readonly IEnumerable<string> args;

        public ConsoleAppConfigProvider(IEnumerable<string> args)
        {
            this.args = args;
        }

        public IAppConfig GetAppConfig()
        {
            var arguments = Parser.Default.ParseArguments<Options>(args).Value;

            var imageSettings = new ImageSettings()
            { 
                Size = new Size(arguments.ImageWidth, arguments.ImageHeight),
                BackgroundColor = Color.FromName(arguments.BackgroundColor),
                FontFamily = new FontFamily(arguments.FontFamily),
                MinFontSize = arguments.MinFontSize,
                MaxFontSize = arguments.MaxFontSize,
                WordColoring = new WordColoringProvider().GetWordColoringByName(arguments.WordColoring)
            };

            return new AppConfig(arguments.InputFileFullPath,
                                 arguments.OutputImageFullPath,
                                 imageSettings);
        }
    }
}
