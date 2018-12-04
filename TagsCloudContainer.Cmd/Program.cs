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
        public string InputFilename { get; set; } = "input.txt";

        public string OutputFilename { get; set; } = "result.png";

        public string FontFamily { get; set; } = "Arial";

        public double FontSize { get; set; } = 12;

        public double SpiralAngleStep { get; set; } = 10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var parametrizedParser = new FluentCommandLineParser<ParserArgs>();
            parametrizedParser.Setup(arg => arg.InputFilename)
                .As("input");

            parametrizedParser.Setup(arg => arg.OutputFilename)
                .As("output");

            parametrizedParser.Setup(arg => arg.FontFamily)
                .As("font");

            parametrizedParser.Setup(arg => arg.FontSize)
                .As("fontSize");

            parametrizedParser.Setup(arg => arg.SpiralAngleStep)
                .As("spiralAngleStep");

            parametrizedParser.Parse(args);

            var parser = new FluentCommandLineParser();
            var callbacks = new CmdCallbacks();

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(callbacks.GetHelpInformation()));

            parser.Setup<string>("imageSize")
                .Callback(imageSize => callbacks.SetImageSize(imageSize));

            parser.Setup<string>("color")
                .Callback(color => callbacks.SetColor(color));

            parser.Setup<string>("spiralOffset")
                .Callback(spiralOffset => callbacks.SetSpiralOffset(spiralOffset));

            parser.Parse(args);

            var cmdArgs = callbacks.CmdArgs;
            var config = new Config(cmdArgs.ImageSize,
                new Font(parametrizedParser.Object.FontFamily, (float)parametrizedParser.Object.FontSize),
                cmdArgs.Color);
            var circularCloudLayoutConfig =
                new CircularCloudLayoutConfig(cmdArgs.SpiralOffset, parametrizedParser.Object.SpiralAngleStep);

            var tagsCloudContainer =
                new ContainerBuilder().BuildTagsCloudContainer(config, circularCloudLayoutConfig);

            var words = tagsCloudContainer
                .Resolve<TxtReader>()
                .GetWords(parametrizedParser.Object.InputFilename);

            tagsCloudContainer
                .Resolve<TagsCloudBuilder>()
                .Visualize(words)
                .Save(parametrizedParser.Object.OutputFilename, ImageFormat.Png);
        }
    }
}