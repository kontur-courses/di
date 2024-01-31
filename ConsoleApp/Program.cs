using System.Reflection;
using Autofac;
using ConsoleApp.Handlers;
using ConsoleApp.Options;
using MyStemWrapper;
using TagsCloudContainer;
using TagsCloudContainer.CloudGenerators;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.FileProviders;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextAnalysers;
using TagsCloudContainer.TextAnalysers.WordsFilters;
using TagsCloudContainer.TextMeasures;
using TagsCloudContainer.Visualizers;

namespace ConsoleApp;

public class Program
{
    public static void Main()
    {
        var builder = new ContainerBuilder();
        ConfigureService(builder);
        var container = builder.Build();

        using var scope = container.BeginLifetimeScope();
        var commandLineReader = scope.Resolve<ICommandLineParser>();
        commandLineReader.ParseFromConsole();
    }

    public static void ConfigureService(ContainerBuilder builder)
    {
        builder.RegisterType<CommandLineParser>().As<ICommandLineParser>();

        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => typeof(IOptions).IsAssignableFrom(t))
            .AsImplementedInterfaces();
        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => typeof(IOptionsHandler).IsAssignableFrom(t))
            .AsImplementedInterfaces();

        var location = Assembly.GetExecutingAssembly().Location;
        var path = Path.GetDirectoryName(location);
        var myStem = new MyStem
        {
            PathToMyStem = $"{path}\\mystem.exe",
            Parameters = "-nli",
        };
        builder.RegisterInstance(myStem).AsSelf().SingleInstance();

        builder.RegisterType<AppSettings>().As<IAppSettings>().SingleInstance();
        builder.RegisterType<AnalyseSettings>().As<IAnalyseSettings>().SingleInstance();
        builder.RegisterType<ImageSettings>().As<IImageSettings>().SingleInstance();

        builder.RegisterType<TextPreprocessor>().As<ITextPreprocessor>();

        builder.RegisterType<TagsCloudGenerator>().As<ITagsCloudGenerator>();
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
        builder.RegisterType<WordsFilter>().As<IWordsFilter>();
        builder.RegisterType<MyStemParser>().As<IMyStemParser>();
        builder.RegisterType<FrequencyCalculator>().As<IFrequencyCalculator>();
        
        builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>();
        builder.RegisterType<TagTextMeasurer>().As<ITagTextMeasurer>();
        builder.RegisterType<ImageProvider>().As<IImageProvider>();
        builder.RegisterType<FileReader>().As<IFileReader>();
        builder.RegisterType<TagsCloudContainer.TagsCloudContainer>().As<ITagsCloudContainer>();
        builder.RegisterType<CloudLayouterProvider>().As<ICloudLayouterProvider>();
    }
}