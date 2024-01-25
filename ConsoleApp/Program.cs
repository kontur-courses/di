using System.Reflection;
using Autofac;
using CommandLine;
using ConsoleApp;
using ConsoleApp.CommandLineParsers.Handlers;
using ConsoleApp.CommandLineParsers.Options;
using MyStemWrapper;
using TagsCloudContainer;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextAnalysers;
using TagsCloudContainer.Visualizers;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        ConfigureService(builder);
        var container = builder.Build();

        using var scope = container.BeginLifetimeScope();
        var commandLineReader = scope.Resolve<CommandLineReader>();
        while (true)
        {
            commandLineReader.Read();
        }
    }

    private static void ConfigureService(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => typeof(IOptions).IsAssignableFrom(t))
            .AsImplementedInterfaces();

        builder.RegisterType<PreprocessTextOptionsHandler>().As<IOptionsHandler<PreprocessTextOptions>, IOptionsHandler>();
        builder.RegisterType<SaveImageOptionsHandler>().As<IOptionsHandler<SaveImageOptions>, IOptionsHandler>();
        builder.RegisterType<ExitOptionsHandler>().As<IOptionsHandler<ExitOptions>, IOptionsHandler>();
        builder.RegisterType<CommandLineReader>().AsSelf();

        var location = Assembly.GetExecutingAssembly().Location;
        var path = Path.GetDirectoryName(location);
        var myStem = new MyStem
        {
            PathToMyStem =
                $"{path}\\mystem.exe",
            Parameters = "-nli"
        };
        builder.RegisterInstance(myStem).AsSelf().SingleInstance();

        builder.RegisterType<CloudData>().AsSelf().SingleInstance();
        builder.RegisterType<ImageSettings>().AsSelf().SingleInstance();
        builder.RegisterType<AnalyseSettings>().AsSelf().SingleInstance();

        builder.RegisterType<TextPreprocessor>().As<ITextPreprocessor>();
        builder.RegisterType<FileReader>().AsSelf();


        builder.RegisterType<TagsCloudGenerator>().As<ITagsCloudGenerator>();
        builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();


        builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>();
        builder.RegisterType<FontSizeProvider>().AsSelf();
    }
}