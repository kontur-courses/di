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

    private static void RegisterVisualizer(ContainerBuilder builder)
    {
        builder.RegisterType<Visualizer>().As<IVisualizer>().AsSelf();
    }

    private static void RegisterStyler(ContainerBuilder builder)
    {
        builder.RegisterType<Styler>().As<IStyler>().AsSelf();
    }

    private static void RegisterTextReader(ContainerBuilder builder)
    {
        builder.RegisterInstance(new StringReader("tag1 Tag1 tag2 tag2 tag2 tAg3 tag3 tag3 tag3 taG4 tag4 tag4 tag4 tag4"))
            .As<ITextReader>().AsSelf();
    }

    private static void RegisterSettingsProviders(ContainerBuilder builder)
    {
        builder.RegisterInstance(new StandartBitmapSettings()).AsSelf().As<ISettingsProvider>().SingleInstance();
        builder.RegisterInstance(new LayouterSettingsProvider()).AsSelf().As<ISettingsProvider>().SingleInstance();
        builder.RegisterInstance(new TextAnalyzerSettings()).AsSelf().As<ISettingsProvider>().SingleInstance();
        builder.RegisterInstance(new DefaultStyle()).AsSelf().As<ISettingsProvider>().SingleInstance();
    }

    private static void RegisterWordFilter(ContainerBuilder builder)
    {
        builder.RegisterType<NoneFilter>().As<IWordFilter>().AsSelf();
    }

    private static void RegisterNormalizer(ContainerBuilder builder)
    {
        builder.RegisterType<LowerNormalizer>().As<IWordNormalizer>().AsSelf();
    }

    private static void RegisterAnalyzer(ContainerBuilder builder)
    {
        builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>().AsSelf();
    }

    private static void RegisterPacker(ContainerBuilder builder)
    {
        builder.RegisterType<TagPacker>().As<ITagPacker>().AsSelf();
    }

    private static void RegisterLayouter(ContainerBuilder builder)
    {
        builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
        builder.RegisterAdapter<LayouterSettingsProvider, CircularCloudLayouter>((cont, p) => cont.Resolve<LayouterSettingsProvider>().Create())
            .As<CircularCloudLayouter>();
    }
}

