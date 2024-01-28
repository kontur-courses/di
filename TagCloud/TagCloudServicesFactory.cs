using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.Excluders;
using TagCloud.TextHandlers;
using TagCloud.WordFilters;
using TagCloudApplication;
using TagCloudTests;

namespace TagCloud;

public static class TagCloudServicesFactory
{
    public static ServiceCollection ConfigureService(Options options)
    {
        var services = new ServiceCollection();
        
        services.AddSingleton<TagCloudGenerator>();
                
        services.AddSingleton<ICloudShaper>(provider => SpiralCloudShaper.Create(new Point(0, 0)));
        services.AddSingleton<CircularCloudLayouter>();
        
        services.AddSingleton<ICloudDrawer>(provider => TagCloudDrawer.Create(
                options.DestinationPath, 
                options.Name, 
                options.FontSize,
                provider.GetService<IColorSelector>()
            )
        );
        if (options.ColorScheme == "random")
            services.AddSingleton<IColorSelector, RandomColorSelector>();
        else
            services.AddSingleton<IColorSelector>(provider => new ConstantColorSelector(Color.Black));

        services.AddSingleton<IWordFilter, MyStemWordFilter>();
                
        services.AddSingleton<ITextHandler>(provider => 
            new FileTextHandler(
                stream: File.Open(options.SourcePath, FileMode.Open), 
                filter: provider.GetService<IWordFilter>()
            )
        );
        return services;
    }

    public static bool ConfigureServiceAndTryGet<T>(Options option, out T value)
    {
        using var serviceProvider = ConfigureService(option).BuildServiceProvider();
        value = serviceProvider.GetService<T>();
        return value != null;
    }
}