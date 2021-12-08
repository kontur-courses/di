using Autofac;
using CommandLine;
using TagCloud.App.UI;

namespace TagCloud;

public static class Program
{
    public static void Main(string[] args)
    {
        var appSettings = ParseAppSettings(args);

        using var container = Startup.BuildDependencies(appSettings);

        container
            .Resolve<IUserInterface>()
            .Run(appSettings);
    }

    private static AppSettings ParseAppSettings(string[] args)
    {
        var parsed = Parser.Default.ParseArguments<AppSettings>(args) as Parsed<AppSettings>;

        if (parsed == null)
            Environment.Exit(-1);

        return parsed.Value;
    }
}