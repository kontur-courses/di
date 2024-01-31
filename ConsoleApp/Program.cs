using System.Reflection;
using Autofac;
using MyStemWrapper;
using TagsCloudContainer;
using TagsCloudContainer.Settings;

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
        RegisterAssemblyTypes(builder, typeof(Tag).GetTypeInfo().Assembly);
        RegisterAssemblyTypes(builder, typeof(CommandLineParser).GetTypeInfo().Assembly);
        
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
    }

    private static void RegisterAssemblyTypes(ContainerBuilder builder, Assembly assembly)
    {
        builder.RegisterAssemblyTypes(assembly)
            .AsImplementedInterfaces();
    }
}