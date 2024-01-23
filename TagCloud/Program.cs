using System.Drawing;
using Autofac;
using TagCloud.CloudDrawer;
using TagCloud.PointGenerator;
using CommandLine;
using TagCloud.CloudSaver;
using TagCloud.FileReader;
using TagCloud.Layouter;
using TagCloud.UserInterface;
using TagCloud.WordFilter;
using TagCloud.WordRanker;
using TagCloud.WordsPreprocessor;

namespace TagCloud;

public class Program
{
    static void Main(string[] args)
    {
        var settings = Parser.Default.ParseArguments<Settings.Settings>(args).Value;
        var builder = new ContainerBuilder();
        builder.RegisterType<FileReader.FileReader>().As<IFileReader>();
        builder.RegisterType<CloudSaver.CloudSaver>().As<ICloudSaver>();
        builder.RegisterType<CloudDrawer.CloudDrawer>().As<IDrawer>();
        builder.RegisterType<WordRankerByFrequency>().As<IWordRanker>();
        builder.RegisterType<WordFilter.WordFilter>().As<IFilter>();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();

        builder.RegisterType<ConsoleUI>().As<IUserInterface>();

        builder.RegisterType<RandomPalette>().As<IPalette>();
        builder.Register(l =>
            new Layouter.Layouter(new SpiralGenerator(new Point(settings.CloudWidth / 2, settings.CloudWidth / 2),
                settings.CloudDensity))).As<ILayouter>();

        builder.Register(s => settings).AsImplementedInterfaces();

        var container = builder.Build();
        container.Resolve<IUserInterface>().Run(settings);
    }
}