using Autofac;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
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
        RegisterBitmapSettings(builder);
        RegisterTextReader(builder);
        RegisterStyler(builder);

        var container = builder.Build();

        var vis = container.Resolve<IVisualizer>(new NamedParameter("Center", new Point(400, 200)));

        var tags = container.Resolve<ITagPacker>();
        var layouter = container.Resolve<ILayouter>();
        var styler = container.Resolve<IStyler>();
        var img = vis.GetBitmap(tags, layouter, styler);
        img.Save("img.png", ImageFormat.Png);

        static void RegisterVisualizer(ContainerBuilder builder) => builder.RegisterType<Visualizer>().As<IVisualizer>();
    }

    private static void RegisterStyler(ContainerBuilder builder)
    {
        builder.RegisterType<Styler>().As<IStyler>();
        builder.RegisterInstance(new DefaultStyle()).As<IStylerSettings>();
    }

    private static void RegisterTextReader(ContainerBuilder builder)
    {
        builder.RegisterInstance(new StringReader("tag1 Tag1 tag2 tag2 tag2 tAg3 tag3 tag3 tag3 taG4 tag4 tag4 tag4 tag4"))
            .As<ITextReader>();
    }

    private static void RegisterBitmapSettings(ContainerBuilder builder)
    {
        builder.RegisterType<StandartBitmapSettings>().As<IBitmapSettingsProvider>();
    }

    private static void RegisterWordFilter(ContainerBuilder builder)
    {
        builder.RegisterType<NoneFilter>().As<IWordFilter>();
    }

    private static void RegisterNormalizer(ContainerBuilder builder)
    {
        builder.RegisterType<LowerNormalizer>().As<IWordNormalizer>();
    }

    private static void RegisterAnalyzer(ContainerBuilder builder)
    {
        builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>()
            .WithParameter(new TypedParameter(typeof(char[]), new[] {' '}));
    }

    private static void RegisterPacker(ContainerBuilder builder)
    {
        builder.RegisterType<TagPacker>().As<ITagPacker>();
    }

    private static void RegisterLayouter(ContainerBuilder builder)
    {
        builder.RegisterType<CircularCloudLayouter.CircularCloudLayouter>()
                    .As<ILayouter>()
                    .WithParameter(new TypedParameter(typeof(Point), new Point(400, 200)));
    }
}

