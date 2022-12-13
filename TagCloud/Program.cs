using System.Drawing;
using System.Globalization;
using Autofac;
using TagCloud.App.CloudCreatorDriver.CloudCreator;
using TagCloud.App.CloudCreatorDriver.CloudDrawers;
using TagCloud.App.CloudCreatorDriver.CloudDrawers.DrawingSettings;
using TagCloud.App.CloudCreatorDriver.ImageSaver;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters;
using TagCloud.App.CloudCreatorDriver.RectanglesLayouters.SpiralCloudLayouters;
using TagCloud.App.WordPreprocessorDriver.InputStream;
using TagCloud.App.WordPreprocessorDriver.InputStream.TextSplitters;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
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
        builder.RegisterType<ConsoleClient>().As<IClient>();
        builder.RegisterType<Phrases>();
        
        builder.RegisterType<TxtEncoder>().As<IFileEncoder>();
        builder.RegisterType<FromFileInputWordsStream>();
        builder.RegisterType<NewLineTextSplitter>().As<ITextSplitter>();
        builder.RegisterType<BoringUnionsAndAppealsRu>().As<IBoringWords>();
        builder.RegisterType<DefaultWordsPreprocessor>().As<IWordsPreprocessor>();
        
        builder.RegisterType<SpiralCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterInstance(new SpiralCloudLayouterSettings(new Point(0,0), 1, 0.1))
            .As<ICloudLayouterSettings>().SingleInstance();
        
        builder.RegisterType<CloudDrawer>().As<ICloudDrawer>();
        builder.RegisterType<DrawingSettings>().As<IDrawingSettings>().SingleInstance();
        builder.RegisterInstance(new LinearWordVisualisationSelector("arial", 13, 30))
            .As<IWordsVisualisationSelector>().SingleInstance();
        
        builder.RegisterType<CloudCreator>().As<ICloudCreator>();
        builder.RegisterType<PngImageSaver>().As<IImageSaver>();
        builder.RegisterInstance(CultureInfo.CurrentCulture).As<CultureInfo>();
        builder.RegisterInstance(GetDefaultWordVisualization()).As<IWordVisualisation>();

        return builder.Build();
    }

    private static IWordVisualisation GetDefaultWordVisualization()
    {
        return new WordVisualisation(Color.Black, 0, new Font("arial", 15));
    }
}