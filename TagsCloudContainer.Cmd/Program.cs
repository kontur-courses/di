using System;
using System.Drawing;
using System.Drawing.Imaging;
using Autofac;
using Fclp;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsReaders;

namespace TagsCloudContainer.Cmd
{
    public class ParserArgs
    {
        public string InputFilename { get; set; }

        public string OutputFilename { get; set; } = "result.png";

        public string FontFamily { get; set; } = "Arial";

        public double FontSize { get; set; } = 12;

        public double SpiralAngleStep { get; set; } = 10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser<ParserArgs>();
            parser.Setup(arg => arg.InputFilename)
                .As("input")
                .Required();

            parser.Setup(arg => arg.OutputFilename)
                .As("output");

            parser.Setup(arg => arg.FontFamily)
                .As("font");

            parser.Setup(arg => arg.FontSize)
                .As("fontSize");

            parser.Setup(arg => arg.SpiralAngleStep)
                .As("spiralAngleStep");

            var callbacks = new CmdCallbacks();

            parser.Parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(callbacks.GetHelpInformation()));

            parser.Parser.Setup<string>("imageSize")
                .Callback(imageSize => callbacks.SetImageSize(imageSize));

            parser.Parser.Setup<string>("color")
                .Callback(color => callbacks.SetColor(color));

            parser.Parser.Setup<string>("spiralOffset")
                .Callback(spiralOffset => callbacks.SetSpiralOffset(spiralOffset));

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

            var cmdArgs = callbacks.CmdArgs;
            var config = new Config(cmdArgs.ImageSize,
                new Font(parser.Object.FontFamily, (float)parser.Object.FontSize),
                cmdArgs.Color);
            var circularCloudLayoutConfig =
                new CircularCloudLayoutConfig(cmdArgs.SpiralOffset, parser.Object.SpiralAngleStep);

            var tagsCloudContainer =
                new ContainerBuilder().BuildTagsCloudContainer(config, circularCloudLayoutConfig);

            var words = tagsCloudContainer
                .Resolve<TxtReader>()
                .GetWords(parser.Object.InputFilename);

            using (var scope = tagsCloudContainer.BeginLifetimeScope())
            {
                scope
                    .Resolve<TagsCloudBuilder>()
                    .Visualize(words)
                    .Save(parser.Object.OutputFilename, ImageFormat.Png);
            }
        }
    }
}