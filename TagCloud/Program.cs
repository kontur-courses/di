using DocoptNet;
using System.Drawing;
using Autofac;
using TagsCloudVisualization;

namespace TagCloud
{
    class Program
    {
        private const string usage = @"
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
            var arguments = new Docopt().Apply(usage, args, exit:true);
            var input = arguments["--input"].ToString();
            var output = arguments["--output"].ToString();
            var containerBuilder = new Autofac.ContainerBuilder();
            containerBuilder.RegisterInstance(new TxtTextReader(input)).As<ITextReader>();
            containerBuilder.RegisterType<SimpleWordParser>().As<IWordParser>();
            containerBuilder.RegisterType<SimlpeWordChanger>().As<IWordChanger>();
            containerBuilder.RegisterType<TextParser>().As<ITextParcer>();
            containerBuilder.RegisterInstance(new CircularCloudLayouter(new Point(300, 300), 0.1))
                .As<CircularCloudLayouter>()
                .SingleInstance();
            containerBuilder.RegisterInstance(new Visualiser(new Size(5000, 5000), output))
                .As<Visualiser>()
                .SingleInstance();
            containerBuilder.RegisterType<ConsoleAplication>();
            var container = containerBuilder.Build();
            var app = container.Resolve<ConsoleAplication>();
            app.Run();

        }
    }
}
