using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Autofac;
using Fclp;
using TagsCloudContainer.WordsReaders;

namespace TagsCloudContainer.Cmd
{
    public class ParserArgs
    {
        public string InputFilename { get; set; }

        public string OutputFilename { get; set; } = "result.png";

        public string ExcludeFilename { get; set; }

        public string FontFamily { get; set; } = "Arial";

        public double FontSize { get; set; } = 12;

        public double SpiralAngleStep { get; set; } = 10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<ParserArgs>();

            var cmdArgs = ConfigureParser(parser);

            var parserResult = parser.Parse(args);

            if (parserResult.HasErrors)
            {
                Console.WriteLine(parserResult.ErrorText);

                return;
            }

            if (parserResult.HelpCalled)
            {
                return;
            }

            var containerBuilder = new CloudContainerBuilder();

            var tagsCloudContainer = containerBuilder.BuildTagsCloudContainer();
            var reader = tagsCloudContainer
                .Resolve<IWordsReader>();

            var font = new Font(parser.Object.FontFamily, (float)parser.Object.FontSize);

            var boringWords = Enumerable.Empty<string>();

            if (parser.Object.ExcludeFilename != null)
            {
                boringWords = reader
                    .GetWords(parser.Object.ExcludeFilename);
            }

            var words = reader
                .GetWords(parser.Object.InputFilename);

            using (var scope = tagsCloudContainer.BeginLifetimeScope())
            {
                var config = scope.Resolve<Config>();
                config.Color = cmdArgs.Color;
                config.Font = font;
                config.CustomBoringWords = boringWords;
                config.ImageSize = cmdArgs.ImageSize;
                config.CenterPoint = cmdArgs.SpiralOffset;
                config.AngleDelta = parser.Object.SpiralAngleStep;

                scope.Resolve<TagsCloudBuilder>()
                    .Visualize(words)
                    .Save(parser.Object.OutputFilename, ImageFormat.Png);
            }
        }

        private static CmdArguments ConfigureParser(FluentCommandLineParser<ParserArgs> parser)
        {
            var callbacks = new CmdCallbacks();
            ;

            parser.Setup(arg => arg.InputFilename)
                .As("input")
                .Required();

            parser.Setup(arg => arg.OutputFilename)
                .As("output");

            parser.Setup(arg => arg.ExcludeFilename)
                .As("exclude");

            parser.Setup(arg => arg.FontFamily)
                .As("font");

            parser.Setup(arg => arg.FontSize)
                .As("fontSize");

            parser.Setup(arg => arg.SpiralAngleStep)
                .As("spiralAngleStep");

            parser.Parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(callbacks.GetHelpInformation()));

            parser.Parser.Setup<string>("imageSize")
                .Callback(callbacks.SetImageSize);

            parser.Parser.Setup<string>("color")
                .Callback(callbacks.SetColor);

            parser.Parser.Setup<string>("spiralOffset")
                .Callback(callbacks.SetSpiralOffset);

            return callbacks.CmdArgs;
        }
    }
}