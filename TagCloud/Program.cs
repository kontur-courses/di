using System;
using System.Collections.Generic;
using DocoptNet;
using System.Drawing;
using System.Linq;
using Autofac;
using TagsCloudVisualization;

namespace TagCloud
{
    class Program
    {
        private static readonly Point CloudCenter = new Point(300, 300);
        private static readonly Size PictureSize = new Size(10000, 10000);
        private const string Usage = @"
    Usage:
      TagCloud.exe --input=<file> --output=<file> [--bgColor=<color>] [--textColor=<color>] [--sqrt | --trgl]
      TagCloud.exe (-h | --help)

    Options:
      -h --help           Показывает подсказку по использованию.
      --input file        Выбор файла для обрабоки.(Поддерживает текстовые форматы: txt, doc, docx).
      --output file       Выбор файла для вывода результата..
      --bgColor color     Выбор цвета фона в шестнадцатиричном формате.
      --textColor color   Выбор цвета текста в шестнадцатирочном формате.
      --sqrt              Форма облака - квадрат.
      --trgl              Форма облака - треугольник.

    ";

        static void Main(string[] args)
        {
            var arguments = ParseArguments(args);
            var bgColor = arguments["--bgColor"];
            var textColor = arguments["--textColor"];
            IPointsMaker pointsMaker;
            if (arguments["--form"] == "sqrt") 
                pointsMaker = new SquarePointsMaker();
            else if (arguments["--form"] == "trgl")
                pointsMaker = new TrianglePointsMaker();
            else pointsMaker = new ArchimedesSpiralPointsMaker();

            ITextReader textReader;
            if (arguments["--fileFormat"] == "doc" 
                || arguments["--fileFormat"] == "docx")
                textReader = new DocTextReader();
            else textReader = new TxtTextReader();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(textReader).As<ITextReader>();
            containerBuilder.RegisterType<SimpleWordParser>().As<IWordParser>();
            containerBuilder.RegisterType<SimpleWordChanger>().As<IWordChanger>();
            containerBuilder.RegisterType<TextParser>().As<ITextParcer>();
            containerBuilder.RegisterInstance(new CloudLayouter(CloudCenter, 1, pointsMaker))
                .As<ICloudLayouter>()
                .SingleInstance();
            containerBuilder.RegisterInstance(new Visualizer(PictureSize, bgColor, textColor))
                .As<Visualizer>()
                .SingleInstance();
            containerBuilder.RegisterType<ConsoleApplication>();
            var container = containerBuilder.Build();

            var app = container.Resolve<ConsoleApplication>();
            var input = arguments["--input"];
            var output = arguments["--output"];
            app.Run(input, output);
        }

        private static Dictionary<string, string> ParseArguments(string[] args)
        {
            var parsedArguments = new Dictionary<string, string>();
            var arguments = new Docopt().Apply(Usage, args, exit: true);
            parsedArguments.Add("--input", arguments["--input"].ToString());
            parsedArguments.Add("--output", arguments["--output"].ToString());
            var inputFormat = parsedArguments["--input"]
                .Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last();
            parsedArguments.Add("--fileFormat", inputFormat);
            if (arguments["--sqrt"].IsTrue)
                parsedArguments.Add("--form", "sqrt");
            else if (arguments["--trgl"].IsTrue)
                parsedArguments.Add("--form", "trgl");
            else parsedArguments.Add("--form", "circle");
            parsedArguments.Add("--bgColor",
                arguments["--bgColor"] != null ? arguments["--bgColor"].ToString() : "#FFFFFF");
            parsedArguments.Add("--textColor",
                arguments["--textColor"] != null ? arguments["--textColor"].ToString() : "#000000");
            return parsedArguments;
        }
    }
}
