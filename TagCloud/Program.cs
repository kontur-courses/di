using System.Drawing;
using Autofac;
using TagCloud.Drawer;
using TagCloud.FileReader;
using TagCloud.FileSaver;
using TagCloud.Filter;
using TagCloud.Layouter;
using TagCloud.PointGenerator;
using TagCloud.UserInterface;
using TagCloud.WordRanker;
using TagCloud.WordsPreprocessor;

namespace TagCloud;

public class Program
{
    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        var settings = Configurator.Parse(args, builder);
        builder.RegisterType<TxtReader>().As<IFileReader>();
        builder.RegisterType<ImageSaver>().As<ISaver>();
        builder.RegisterType<CloudDrawer>().As<IDrawer>();
        builder.RegisterType<WordRankerByFrequency>().As<IWordRanker>();
        builder.RegisterType<DefaultPreprocessor>().As<IPreprocessor>();

        builder.RegisterType<ConsoleUI>().As<IUserInterface>();

        builder.Register(c => new WordFilter().UsingFilter((word) => word.Length > 3)).As<IFilter>();
        builder.Register(c =>
            new CircularLayouter(new SpiralGenerator(new Point(settings.CloudWidth / 2, settings.CloudWidth / 2),
                settings.CloudDensity))).As<ILayouter>();

        builder.Register(c => settings).AsImplementedInterfaces();

        var container = builder.Build();
        container.Resolve<IUserInterface>().Run(settings);
    }
}