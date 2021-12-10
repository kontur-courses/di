using Autofac;
using TagCloud.App.UI;
using TagCloud.App.UI.Console.Common;

namespace TagCloud;

public static class Program
{
    public static void Main(string[] args)
    {
        var appSettings = AppSettings.Parse(args);
        var builder = new ContainerBuilder();
        
        using var container = builder
            .ConfigureConsoleClient(appSettings)
            .Build();
        container
            .Resolve<IUserInterface>()
            .Run(appSettings);
    }
}