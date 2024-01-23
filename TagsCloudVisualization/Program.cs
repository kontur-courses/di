using System.ComponentModel.DataAnnotations;
using System.Drawing;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudVisualization;

public class Program
{
    public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    [Argument(0)] [Required] public string InputFilePath { get; set; }

    [Argument(1)] [Required] public string OutputFileName { get; set; }

    [Option("-o")] public string? OutputDirectory { get; set; }

    [Option("-w")] public int ImageWidth { get; set; } = 1000;
    
    [Option("-h")] public int ImageHeight { get; set; } = 1000;

    [Option("-bc")] public Color BackgroundColor { get; set; } = Color.Wheat;

    [Option("-tc")] public Color TextColor { get; set; } = Color.Black;

    [Option("-ff")] public string FontFamily { get; set; }

    [Option("-fs")] public int FontSize { get; set; } = 50;


    private void OnExecute()
    {
        var services = new ServiceCollection();
        services.AddSingleton<Font>(x => new Font(FontFamily, FontSize));
        services.AddSingleton<Palette>(x => new Palette(TextColor, BackgroundColor));
        services.AddSingleton<IPointGenerator, SwampPointGenerator>();
        services.AddSingleton<IWordParser, WordParser>();
        services.AddSingleton<IDullWordChecker, NoWordsDullChecker>();
        services.AddSingleton<ICloudLayouter>(x =>
            new CloudLayouter(x.GetRequiredService<IPointGenerator>(),
                x.GetRequiredService<Font>(),
                x.GetRequiredService<IWordParser>()
                    .GetInterestingWords(InputFilePath, x.GetRequiredService<IDullWordChecker>())));

        using var provider = services.BuildServiceProvider();

        LayoutDrawer.CreateLayoutImage(provider.GetRequiredService<ICloudLayouter>().CreateLayout(),
            new Size(ImageWidth, ImageHeight),
            provider.GetRequiredService<Palette>(),
            OutputFileName,
            OutputDirectory);
    }
}