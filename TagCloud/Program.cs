using Autofac;
using TagCloud.App.UI;
using TagCloud.App.UI.Common;

namespace TagCloud;

public static class Program
{
    public static void Main(string[] args)
    {
        var appSettings = AppSettings.Parse(args);
        using var container = Startup.BuildDependencies(appSettings);

        container
            .Resolve<IUserInterface>()
            .Run(appSettings);
    }
}