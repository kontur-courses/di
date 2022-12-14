using CommandLine;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.CommandLineParsing;
using TagCloud.ImageProcessing;

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
                WordColoringAlgorithmName = arguments.WordColoring
            };

            return new AppConfig(arguments.InputFileFullPath,
                                 arguments.OutputImageFullPath,
                                 arguments.CloudForm,
                                 new Point(arguments.CentralPointX, arguments.CentralPointY),
                                 imageSettings);
        }
    }
}
