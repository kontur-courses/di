using System.Drawing;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using TagCloud;
using TagCloud.TextHandlers;
using TagCloudTests;

namespace TagCloudApplication;

class Program
{
    static void Main(string[] args)
    {
        using var serviceProvider = ConfigureService(args).BuildServiceProvider();
        
        serviceProvider.GetService<TagCloudGenerator>().Generate();
    }

    private static ServiceCollection ConfigureService(string[] args)
    {
        var services = new ServiceCollection();
        
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                services.AddScoped<TagCloudGenerator>();
                
                services.AddScoped<CircularCloudLayouter>(provider => new CircularCloudLayouter(
                        new Point(0,0),
                        provider.GetService<ICloudShaper>()
                    )
                );
                services.AddScoped<ICloudShaper, SpiralCloudShaper>();
                
                //TODO: file + name, may be directory and names
                services.AddScoped<ICloudDrawer>(provider => TagCloudDrawer.Create(
                        o.File, 
                        "rnd", 
                        provider.GetService<IColorSelector>()
                    )
                );
                if (o.ColorScheme == "random")
                    services.AddScoped<IColorSelector, RandomColorSelector>();
                else
                    services.AddScoped<IColorSelector>(provider => new ConstantColorSelector(Color.Black));
                
                services.AddScoped<ITextHandler, FileTextHandler>();
            });
        return services;
    }
}