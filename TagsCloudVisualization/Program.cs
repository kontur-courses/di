using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudVisualization;

public class Program
{
    public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    [Argument(0)] [Required] public string InputFilePath { get; set; }

    [Argument(1)] [Required] public string OutputFilePath { get; set; }

    [Option("-w")] public int ImageWidth { get; set; } = 1000;

    [Option("-h")] public int ImageHeight { get; set; } = 1000;

    [Option("-bc")] public Color BackgroundColor { get; set; } = Color.Wheat;

    [Option("-tc")] public Color TextColor { get; set; } = Color.Black;

    [Option("-ff")] public string FontFamily { get; set; }

    [Option("-fs")] public int FontSize { get; set; } = 50;


    private void OnExecute()
    {
        var services = new ServiceCollection();
        services.AddTransient<Font>(x => new Font(FontFamily, FontSize, FontStyle.Regular));
        services.AddTransient<Palette>(x => new Palette(TextColor, BackgroundColor));
        services.AddTransient<IPointGenerator, SpiralPointGenerator>();
        services.AddTransient<IDullWordChecker, NoWordsDullChecker>();
        services.AddTransient<IInterestingWordsParser, InterestingWordsParser>();
        services.AddTransient<IRectangleLayouter, RectangleLayouter>();
        services.AddTransient<LayoutDrawer>();

        using var provider = services.BuildServiceProvider();

        var layoutDrawer = provider.GetRequiredService<LayoutDrawer>();
        layoutDrawer
            .CreateLayoutImageFromFile(InputFilePath, new Size(ImageWidth, ImageHeight))
            .SaveImage(OutputFilePath, ImageFormat.Png);
    }
}