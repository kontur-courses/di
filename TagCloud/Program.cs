using System.Drawing;
using System.Globalization;
using Autofac;
using Autofac.Core;
using TagCloud.App.CloudCreatorDriver.CloudCreator;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.ImageSaver;
using TagCloud.App.CloudCreatorDriver.ImageSaver.FileTypes;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.FileInputStream;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;
using TagCloud.Clients;
using TagCloud.Clients.ConsoleClient;
namespace TagCloud;

static class Program
{
    static void Main()
    {
        var container = GetContainer();
        using var scope = container.BeginLifetimeScope();
        var client = scope.Resolve<IClient>();
        client.Run();
    }

    private static IContainer GetContainer()
    {
        var builder = new ContainerBuilder();
        builder
            .RegisterInstance(GetDefaultWordVisualization())
            .As<IWordVisualisation>()
            .SingleInstance();
        builder.RegisterType<ConsoleClient>().As<IClient>();
        builder.RegisterType<CloudCreator>().As<ICloudCreator>();
        builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
        builder.RegisterType<DrawingSettings>().As<IDrawingSettings>();
        builder.RegisterType<PngImageSaver>().As<IImageSaver>();
        builder.RegisterType<SpiralCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<SpiralCloudLayouterSettings>().As<ICloudLayouterSettings>();
        builder.RegisterType<FromFileInputWordsStream>().As<IInputWordsStream>();
        builder.RegisterType<TxtEncoder>().As<IFileEncoder>();
        builder.RegisterType<DefaultWordsPreprocessor>().As<IWordsPreprocessor>();
        builder.RegisterType<Word>().As<IWord>();
        builder.RegisterType<BoringUnionsAndAppealsRu>().As<IBoringWords>();
        builder.RegisterInstance(CultureInfo.CurrentCulture).As<CultureInfo>();

        return builder.Build();
    }

    private static IWordVisualisation GetDefaultWordVisualization()
    {
        return new WordVisualisation(Color.Black, 0, new Font("arial", 3));
    }
}