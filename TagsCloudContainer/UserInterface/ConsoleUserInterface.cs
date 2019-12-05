using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Fclp;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.UserInterface
{
    public class ConsoleUserInterface : IParametersProvider, IResultDisplay
    {
        public bool TryGetParameters(string[] programArgs, out Parameters parameters)
        {
            var parser = SetupParser();

            var parseResult = parser.Parse(programArgs);
            if (!parseResult.HasErrors && !parseResult.HelpCalled)
            {
                try
                {
                    parameters = ParseArgumentsToParameters(parser.Object);
                    return true;
                }
                catch (NotSupportedException notSupportedException)
                {
                    Console.WriteLine(notSupportedException);
                    parameters = null;
                    return false;
                }
            }

            Console.WriteLine(parseResult.ErrorText);
            parameters = null;
            return false;
        }

        public void ShowResult(Bitmap bitmap)
        {
            Console.WriteLine(@"Successfully created tag cloud");
        }

        private FluentCommandLineParser<ConsoleUserInterfaceArguments> SetupParser()
        {
            var parser = new FluentCommandLineParser<ConsoleUserInterfaceArguments>();

            parser.Setup(arg => arg.InputFilePath).As('i', "input").Required()
                .WithDescription("input file path (required)");
            parser.Setup(arg => arg.OutputFilePath).As('o', "output").SetDefault("test.png")
                .WithDescription("output file path, default is test.png");
            parser.Setup(arg => arg.Width).As('w', "width").SetDefault(800)
                .WithDescription("width of image, default is 800");
            parser.Setup(arg => arg.Height).As('h', "height").SetDefault(600)
                .WithDescription("height of image, default is 600");
            parser.Setup(arg => arg.Font).As('f', "font").SetDefault("Arial")
                .WithDescription("name of font, default is Arial");
            parser.Setup(arg => arg.Colors).As("colors").SetDefault(new List<string> {"Aqua", "Black"})
                .WithDescription("names of colors to use, default: Aqua Black");

            parser.SetupHelp("?", "help").UseForEmptyArgs().Callback(text => Console.WriteLine(text))
                .WithHeader("Arguments to use:");

            return parser;
        }

        private Parameters ParseArgumentsToParameters(ConsoleUserInterfaceArguments arguments)
        {
            var imageSize = new Size(arguments.Width, arguments.Height);
            var fontConverter = new FontConverter();
            var font = fontConverter.ConvertFromString(arguments.Font) as Font;
            var colorConverter = new ColorConverter();
            var colors = arguments.Colors.Select(name => colorConverter.ConvertFromString(name)).Cast<Color>().ToList();
            return new Parameters(arguments.InputFilePath, arguments.OutputFilePath, colors, font, imageSize);
        }
    }

    public class ConsoleUserInterfaceArguments
    {
        public string InputFilePath { get; set; }

        public string OutputFilePath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Font { get; set; }

        public List<string> Colors { get; set; }
    }
}