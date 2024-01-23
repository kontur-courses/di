using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.TextHandlers;
using TagCloudApplication;
using TagCloudTests;

namespace TagCloud;

public class TagCloudGenerator
{
    private readonly ITextHandler handler;
    private readonly CircularCloudLayouter layouter;
    private readonly ICloudDrawer drawer;

    public TagCloudGenerator(ITextHandler handler, CircularCloudLayouter layouter, ICloudDrawer drawer)
    {
        this.handler = handler;
        this.layouter = layouter;
        this.drawer = drawer;
    }

    public void Generate()
    {
        var rectangles = new List<TextRectangle>();
        
        foreach (var (word, count) in handler.Handle().OrderByDescending(tuple => tuple.count))
        {
            var fontSize = drawer.FontSize * count;
            var size = drawer.GetTextRectangleSize(word, fontSize);
            rectangles.Add(new TextRectangle(
                layouter.PutNextRectangle(size),
                word,
                new Font(FontFamily.GenericSerif, fontSize)
            ));
        }
        drawer.Draw(rectangles);
    }
    
    public static ServiceCollection ConfigureService(Options options)
    {
        var services = new ServiceCollection();
        
        services.AddScoped<TagCloudGenerator>();
                
        services.AddScoped<ICloudShaper>(provider => SpiralCloudShaper.Create(new Point(0, 0)));
        services.AddScoped<CircularCloudLayouter>(provider => new CircularCloudLayouter(
                new Point(0,0),
                provider.GetService<ICloudShaper>()
            )
        );
        
        services.AddScoped<ICloudDrawer>(provider => TagCloudDrawer.Create(
                options.DestinationPath, 
                options.Name, 
                options.FontSize,
                provider.GetService<IColorSelector>()
            )
        );
        if (options.ColorScheme == "random")
            services.AddScoped<IColorSelector, RandomColorSelector>();
        else
            services.AddScoped<IColorSelector>(provider => new ConstantColorSelector(Color.Black));
                
        services.AddScoped<ITextHandler>(provider => 
            new FileTextHandler(File.Open(options.SourcePath, FileMode.Open))
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