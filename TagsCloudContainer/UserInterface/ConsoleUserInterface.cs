using System;
using System.Collections.Generic;
using System.Drawing;
using Fclp;
using TagsCloudContainer.Core;
using TagsCloudContainer.UserInterface.ArgumentsParsing;

namespace TagsCloudContainer.UserInterface
{
    public class ConsoleUserInterface : IParametersProvider, IResultDisplay
    {
        private readonly IArgumentsParser<ConsoleUserInterfaceArguments> argumentsParser;

        public ConsoleUserInterface(IArgumentsParser<ConsoleUserInterfaceArguments> argumentsParser)
        {
            this.argumentsParser = argumentsParser;
        }

        public bool TryGetParameters(string[] programArgs, out Parameters parameters)
        {
            var parser = SetupParser();

            var parseResult = parser.Parse(programArgs);
            if (!parseResult.HasErrors && !parseResult.HelpCalled)
            {
                parameters = argumentsParser.ParseArgumentsToParameters(parser.Object);
                return true;
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
            parser.Setup(arg => arg.ImageFormat).As('e', "extension").SetDefault("Png")
                .WithDescription("extension of image file, default is Png");

            parser.SetupHelp("?", "help").UseForEmptyArgs().Callback(text => Console.WriteLine(text))
                .WithHeader("Arguments to use:");

            return parser;
        }
    }
}