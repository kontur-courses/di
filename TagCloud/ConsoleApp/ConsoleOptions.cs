using System.Drawing;
using Autofac;
using CommandLine;
using TagCloud;

namespace ConsoleApp;

public class ConsoleOptions
{
    [Option('f', "file", Required = true, HelpText = "Image file to read words")]
    public string File { get; set; }

    [Option('F', "font", Required = false, HelpText = "Font name", Default = "Arial")]
    public string FontName { get; set; }
    
    [Option("minfont", Required = false, Default = 11, HelpText = "Minimum font size")]
    public int MinFont { get; set; }
    
    [Option("maxfont", Required = false, Default = 64, HelpText = "Maximum font size")]
    public int MaxFont { get; set; }
        
    [Option('W', "width", Required = true, HelpText = "Image width")]
    public int Width { get; set; }

    [Option('H', "height", Required = true, HelpText = "Image height")]
    public int Height { get; set; }
    
    [Option('d', "density", Required = false, Default = 0.1, HelpText = "Cloud density")]
    public double Density { get; set; }

    public void Apply(IContainer container)
    {
        ApplySizeOption(container);
        ApplyFontOption(container);
        ApplyFileOption(container);
        var cloudProperties = container.Resolve<CloudProperties>();
        cloudProperties.Density = Density;
    }
    
    private void ApplySizeOption(IComponentContext container)
    {
        var sizeProperties = container.Resolve<SizeProperties>();
        sizeProperties.ImageSize = new Size(Width, Height);
        container.Resolve<CloudProperties>().Center = sizeProperties.ImageCenter;
    }
    
    private void ApplyFontOption(IComponentContext container)
    {
        var fontProperties = container.Resolve<FontProperties>();
        fontProperties.Family = new FontFamily(FontName);
        fontProperties.MinSize = MinFont;
        fontProperties.MaxSize = MaxFont;
    }
    
    private void ApplyFileOption(IComponentContext container)
    {
        container.Resolve<ApplicationProperties>().Path = File;
    }
}