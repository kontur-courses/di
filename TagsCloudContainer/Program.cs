using Autofac;
using CloudLayouter;
using System.Drawing.Imaging;
using TagsCloudContainer.Defaults;
using TagsCloudVisualization.Abstractions;
using StringReader = TagsCloudContainer.Defaults.StringReader;

namespace TagsCloudContainer;
public partial class Program
{
    public static void Main()
    {
        var builder = new ContainerBuilder();
        RegisterVisualizer(builder);
        RegisterLayouter(builder);
        RegisterPacker(builder);
        RegisterAnalyzer(builder);
        RegisterNormalizer(builder);
        RegisterWordFilter(builder);
        RegisterSettingsProviders(builder);
        RegisterTextReader(builder);
        RegisterStyler(builder);

        var container = builder.Build();

        var tags = container.Resolve<TagPacker>();
        var layouter = container.Resolve<CircularCloudLayouter>();
        var styler = container.Resolve<Styler>();
        var visualizerSettings = container.Resolve<StandartBitmapSettings>();

        var vis = new Visualizer(visualizerSettings, tags, layouter, styler);
        var img = vis.GetBitmap();
        img.Save("img.png", ImageFormat.Png);
    }

    private static void Register<TImpl, TInterface>(ContainerBuilder builder, bool singleton = false)
        where TImpl : TInterface
        where TInterface : notnull
    {
        var registration = builder.RegisterType<TImpl>().As<TInterface>().AsSelf();
        if (singleton)
            registration.SingleInstance();
    }

    private static void RegisterVisualizer(ContainerBuilder builder)
    {
        Register<Visualizer, IVisualizer>(builder);
    }

    private static void RegisterStyler(ContainerBuilder builder)
    {
        Register<Styler, IStyler>(builder);
    }

    private static void RegisterTextReader(ContainerBuilder builder)
    {
        builder.RegisterInstance(new StringReader("tag1 Tag1 tag2 tag2 tag2 tAg3 tag3 tag3 tag3 taG4 tag4 tag4 tag4 tag4"))
            .As<ITextReader>().AsSelf();
    }

    private static void RegisterSettingsProviders(ContainerBuilder builder)
    {
        Register<StandartBitmapSettings, ISettingsProvider>(builder, true);
        Register<LayouterSettingsProvider, ISettingsProvider>(builder, true);
        Register<TextAnalyzerSettings, ISettingsProvider>(builder, true);
        Register<DefaultStyle, ISettingsProvider>(builder, true);
    }

    private static void RegisterWordFilter(ContainerBuilder builder)
    {
        Register<NoneFilter, IWordFilter>(builder);
    }

    private static void RegisterNormalizer(ContainerBuilder builder)
    {
        Register<LowerNormalizer, IWordNormalizer>(builder);
        Register<Capitalizer, IWordNormalizer>(builder);
    }

    private static void RegisterAnalyzer(ContainerBuilder builder)
    {
        Register<TextAnalyzer,ITextAnalyzer>(builder);
    }

    private static void RegisterPacker(ContainerBuilder builder)
    {
        Register<TagPacker,ITagPacker>(builder);
    }

    private static void RegisterLayouter(ContainerBuilder builder)
    {
        builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
        builder.RegisterAdapter<LayouterSettingsProvider, CircularCloudLayouter>((cont, p) => cont.Resolve<LayouterSettingsProvider>().Create())
            .As<CircularCloudLayouter>();
    }
}

