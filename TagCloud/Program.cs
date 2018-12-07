using System.Collections.Generic;
using DocoptNet;
using System.Drawing;
using Autofac;
using TagsCloudVisualization;

namespace TagCloud
{
    class Program
    {
        private static readonly Point CloudCenter = new Point(300, 300);
        private static readonly Size PictureSize = new Size(5000, 5000);
        private const string Usage = @"
    Usage:
      TagCloud.exe --input=<file> --output=<file>
      TagCloud.exe (-h | --help)

    Options:
      -h --help           Show this screen.
      --input file        Chose input text file.
      --output file       Chose output image File.

    ";

        static void Main(string[] args)
        {
            var arguments = ParseArguments(args);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(new TxtTextReader()).As<ITextReader>();
            containerBuilder.RegisterType<SimpleWordParser>().As<IWordParser>();
            containerBuilder.RegisterType<SimpleWordChanger>().As<IWordChanger>();
            containerBuilder.RegisterType<TextParser>().As<ITextParcer>();
            containerBuilder.RegisterInstance(new CircularCloudLayouter(CloudCenter, 0.1))
                .As<ICloudLayouter>()
                .SingleInstance();
            containerBuilder.RegisterInstance(new Visualizer(PictureSize))
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
            return parsedArguments;
        }
    }
}
