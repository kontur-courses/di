using Autofac;
using CloudLayouter;
using CommandLine;
using System.Drawing.Imaging;
using TagsCloudContainer.Defaults;
using TagsCloudVisualization.Abstractions;
using StringReader = TagsCloudContainer.Defaults.StringReader;

namespace TagsCloudContainer;
public class Program
{
    public class Options
    {
        private const string inputGroup = "input";
        [Option('f', "files", HelpText = "Input files to be processed. This takes precedence over '-s' '--string' option", Group = inputGroup)]
        public IEnumerable<string>? InputFiles { get; set; }

        [Option('s', "string", HelpText = "Input string to be processed.", Group = inputGroup)]
        public string? InputString { get; set; }
    }
    public static void Main(string[] args)
    {
        args = new[] { "-s", "tag1 Tag1 tag3 Tag2 tag1 TAG3 tag3 tag4 tag2 tag1" };
        var builder = new ContainerBuilder();
        RegisterAll(builder);
        var container = builder.Build();

        Parser.Default.ParseArguments<Options>(args).WithParsed(opts => Run(opts, container));
    }

    private static void Run(Options obj, IContainer container)
    {
        ITextReader? textReader;
        if (obj.InputFiles != null && obj.InputFiles.Any())
        {
            textReader = container.Resolve<FileReader>(TypedParameter.From(obj.InputFiles.ToArray()));
        }
        else if (obj.InputString != null && !string.IsNullOrWhiteSpace(obj.InputString))
        {
            textReader = container.Resolve<StringReader>(TypedParameter.From(obj.InputString));
        }
        else
        {
            textReader = new StringReader(string.Empty);
        }

        var textAnalyzer = container.Resolve<TextAnalyzer>(TypedParameter.From(textReader));
        var tags = container.Resolve<TagPacker>(TypedParameter.From<ITextAnalyzer>(textAnalyzer));
        var layouter = container.Resolve<CircularCloudLayouter>();
        var styler = container.Resolve<Styler>();
        var visualizerSettings = container.Resolve<BitmapSetting>();

        var vis = new Visualizer(visualizerSettings, tags, layouter, styler);
        var img = vis.GetBitmap();
        img.Save("img.png", ImageFormat.Png);
    }

    private static void RegisterAll(ContainerBuilder builder)
    {
        RegisterVisualizer(builder);
        RegisterLayouter(builder);
        RegisterPacker(builder);
        RegisterAnalyzer(builder);
        RegisterNormalizer(builder);
        RegisterWordFilter(builder);
        RegisterSettingsProviders(builder);
        RegisterTextReader(builder);
        RegisterStyler(builder);
    }

    private static void Register<TImplementation, TInterface>(ContainerBuilder builder, bool singleton = false)
        where TImplementation : TInterface
        where TInterface : notnull
    {
        var registration = builder.RegisterType<TImplementation>()
            .As<TInterface>()
            .AsSelf();
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
        Register<StringReader, ITextReader>(builder);
        Register<FileReader, ITextReader>(builder);
    }

    private static void RegisterSettingsProviders(ContainerBuilder builder)
    {
        Register<BitmapSetting, ISettingsProvider>(builder, true);
        Register<BigBitmapSetting, ISettingsProvider>(builder, true);
        Register<BigBitmapSetting, BitmapSetting>(builder, true);
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
        Register<TextAnalyzer, ITextAnalyzer>(builder);
    }

    private static void RegisterPacker(ContainerBuilder builder)
    {
        Register<TagPacker, ITagPacker>(builder);
    }

    private static void RegisterLayouter(ContainerBuilder builder)
    {
        builder.RegisterType<CircularCloudLayouter>().As<ILayouter>();
        builder.RegisterAdapter<LayouterSettingsProvider, CircularCloudLayouter>((cont, p) => cont.Resolve<LayouterSettingsProvider>().Create())
            .As<CircularCloudLayouter>();
    }
}

