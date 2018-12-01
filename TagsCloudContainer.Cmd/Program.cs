using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Fclp;
using TagsCloudContainer.WordLayouts;

namespace TagsCloudContainer.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new FluentCommandLineParser();
            var callbacks = new CmdCallbacks();

            parser.SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(callbacks.GetHelpInformation()));

            parser.Setup<string>("input")
                .Callback(filename => callbacks.SetInputFilename(filename));

            parser.Setup<string>("output")
                .Callback(filename => callbacks.SetOutputFilename(filename));

            parser.Setup<string>("imageSize")
                .Callback(imageSize => callbacks.SetImageSize(imageSize));

            parser.Setup<string>("font")
                .Callback(font => callbacks.SetFont(font));

            parser.Setup<double>("fontSize")
                .Callback(size => callbacks.SetFontSize(size));

            parser.Setup<string>("color")
                .Callback(color => callbacks.SetColor(color));

            parser.Setup<double>("spiralAngleStep")
                .Callback(spiralAngleStep => callbacks.SetSpiralAngleStep(spiralAngleStep));

            parser.Setup<string>("spiralOffset")
                .Callback(spiralOffset => callbacks.SetSpiralOffset(spiralOffset));

            parser.Parse(args);

            var cmdArgs = callbacks.CmdArgs;
            var config = new Config(cmdArgs.ImageSize, new Font(cmdArgs.FontFamily, cmdArgs.FontSize), cmdArgs.Color);
            var circularCloudLayoutConfig =
                new CircularCloudLayoutConfig(cmdArgs.SpiralOffset, cmdArgs.SpiralAngleStep);

            var tagsCloudContainer =
                new ContainerBuilder().BuildTagsCloudContainer(config, circularCloudLayoutConfig);

            tagsCloudContainer.Visualize(cmdArgs.InputFilename, cmdArgs.OutputFilename);
        }
    }
}