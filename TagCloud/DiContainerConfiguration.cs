using System.Drawing;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.Clients;
using TagCloud.Curves;
using TagCloud.Files;
using TagCloud.Savers;
using TagCloud.Words;

namespace TagCloud;

public static class DiContainerConfiguration
{
    internal static ServiceProvider Build()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<Random>();
        services.AddSingleton<CloudDrawer>();
        services.AddSingleton<CloudLayouter>();
        services.AddSingleton<TextFormatter>();
        services.AddSingleton<IBitmapSaver, HardDriveSaver>();
        services.AddSingleton<Client>();
        return services.BuildServiceProvider();
    }
}