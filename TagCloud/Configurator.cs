using Autofac;
using TagCloud.App.UI;
using TagCloud.App.UI.Console;
using TagCloud.App.UI.Console.Common;
using TagCloud.Infrastructure.FileReader;
using TagCloud.Infrastructure.Filter;
using TagCloud.Infrastructure.Layouter;
using TagCloud.Infrastructure.Lemmatizer;
using TagCloud.Infrastructure.Painter;
using TagCloud.Infrastructure.Saver;
using TagCloud.Infrastructure.Weigher;

namespace TagCloud;

internal static class Configurator
{
    public static ContainerBuilder ConfigureConsoleClient(this ContainerBuilder builder, IAppSettings appSettings)
    {
        builder.RegisterType<ConsoleUI>().As<IUserInterface>();
        builder.RegisterType<RandomPalette>().As<IPalette>();
        builder.RegisterType<Painter>().As<IPainter>();
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().AsSelf();
        builder.RegisterType<ImageSaver>().As<IImageSaver>().SingleInstance();
        builder.RegisterType<WordWeigher>().As<IWordWeigher>().SingleInstance();
        builder.RegisterType<RussianLemmatizer>().As<ILemmatizer>().SingleInstance();
        builder.RegisterType<DocFileReader>().As<IFileReader>().SingleInstance();
        builder.RegisterType<PlainTextFileReader>().As<IFileReader>().AsSelf().SingleInstance();

        builder.Register(c => appSettings).AsImplementedInterfaces().SingleInstance();
        builder.Register(c => new Filter().AddCondition(AuxiliaryPartOfSpechCondition.Filter))
            .As<IFilter>().SingleInstance();
        builder.Register(c => 
                new FileReaderFactory(c.Resolve<IEnumerable<IFileReader>>(), c.Resolve<PlainTextFileReader>()))
            .As<IFileReaderFactory>().SingleInstance();
        builder.Register(c =>
                new CloudLayouterFactory(c.Resolve<IEnumerable<ICloudLayouter>>(), c.Resolve<CircularCloudLayouter>()))
            .As<ICloudLayouterFactory>();

        return builder;
    }
}