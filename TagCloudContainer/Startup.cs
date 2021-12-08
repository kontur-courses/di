using Autofac;
using TagCloud.App.UI;
using TagCloud.App.UI.Common;
using TagCloud.Infrastructure.FileReader;
using TagCloud.Infrastructure.Filter;
using TagCloud.Infrastructure.Layouter;
using TagCloud.Infrastructure.Lemmatizer;
using TagCloud.Infrastructure.Painter;
using TagCloud.Infrastructure.Saver;
using TagCloud.Infrastructure.Weigher;

namespace TagCloud;

internal static class Startup
{
    public static IContainer BuildDependencies(IAppSettings appSettings)
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
}