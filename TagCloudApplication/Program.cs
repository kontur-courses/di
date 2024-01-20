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
                
                services.AddScoped<ICloudShaper>(provider => SpiralCloudShaper.Create(new Point(0, 0)));
                services.AddScoped<CircularCloudLayouter>(provider => new CircularCloudLayouter(
                        new Point(0,0),
                        provider.GetService<ICloudShaper>()
                    )
                );
                
                //TODO: file + name, may be directory and names
                services.AddScoped<ICloudDrawer>(provider => TagCloudDrawer.Create(
                        o.DestinationPath, 
                        o.Name, 
                        o.FontSize,
                        provider.GetService<IColorSelector>()
                    )
                );
                if (o.ColorScheme == "random")
                    services.AddScoped<IColorSelector, RandomColorSelector>();
                else
                    services.AddScoped<IColorSelector>(provider => new ConstantColorSelector(Color.Black));
                
                services.AddScoped<ITextHandler>(provider => 
                    new FileTextHandler(File.Open(o.SourcePath, FileMode.Open))
                );
            });
        return services;
    }
}