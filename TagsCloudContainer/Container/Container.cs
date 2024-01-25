using System.Drawing;
using Autofac;
using TagsCloudContainer.App;
using TagsCloudContainer.App.Interfaces;
using TagsCloudContainer.DrawRectangle;
using TagsCloudContainer.Cloud;
using TagsCloudContainer.DrawRectangle.Interfaces;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainer;

public static class Container
{
    public static IContainer SetDiBuilder(CommandLineOptions options)
    {
        var builder = new ContainerBuilder();
        builder.RegisterInstance(new Settings()
            {
                Color = Color.FromName(options.Color),
                FontName = options.FontName,
                FontSize = options.FontSize,
                File = options.PathToInputFile,
                BoringWordsFileName = options.PathToBoringWordsFile
            })
            .As<Settings>();
        builder.RegisterType<FileReaderFactory>().AsSelf();
        builder.RegisterType<WordProcessor>().AsSelf();
        builder.Register(x =>
                new CircularCloudLayouter(new Point(options.CenterX, options.CenterY)))
            .As<CircularCloudLayouter>();
        builder.RegisterType<RectangleDraw>().As<IDraw>();
        builder.RegisterType<ConsoleApp>().As<IApp>();

        return builder.Build();
    }
}