using Autofac;
using CommandLine;
using TagCloudContainer.App.UI;
using TagCloudContainer.Infrastructure.Common;
using TagCloudContainer.Infrastructure.FileReader;
using TagCloudContainer.Infrastructure.Filter;
using TagCloudContainer.Infrastructure.Layouter;
using TagCloudContainer.Infrastructure.Lemmatizer;
using TagCloudContainer.Infrastructure.Painter;
using TagCloudContainer.Infrastructure.Saver;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainer;

public static class Program
{
    public static void Main(string[] args)
    {
        var appSettings = ParseAppSettings(args);

        BuildDependencies(appSettings)
            .Resolve<IUserInterface>()
            .Run(appSettings);
    }

    private static IContainer BuildDependencies(IAppSettings appSettings)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ConsoleUI>().As<IUserInterface>();
        builder.RegisterType<RandomPalette>().As<IPalette>();
        builder.RegisterType<Painter>().As<IPainter>();
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<ImageSaver>().As<IImageSaver>().SingleInstance();
        builder.RegisterType<WordWeigher>().As<IWordWeigher>().SingleInstance();
        builder.RegisterType<RussianLemmatizer>().As<ILemmatizer>().SingleInstance();
        builder.RegisterType<PlainTextFileReader>().As<IFileReader>().SingleInstance();
        builder.Register(c => new Filter().AddCondition(AuxiliaryPartOfSpechCondition.Filter))
            .As<IFilter>().SingleInstance();
        builder.Register(c => appSettings).As<IAppSettings>().SingleInstance();

        return builder.Build();
    }

    private static AppSettings ParseAppSettings(string[] args)
    {
        var parsed = Parser.Default.ParseArguments<AppSettings>(args) as Parsed<AppSettings>;

        if (parsed == null)
            Environment.Exit(-1);

        return parsed.Value;
    }
}