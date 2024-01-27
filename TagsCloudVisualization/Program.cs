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

    [Option("-w")] private int ImageWidth { get; set; } = 1000;

    [Option("-h")] private int ImageHeight { get; set; } = 1000;

    [Option("-bc")] private Color BackgroundColor { get; set; } = Color.Wheat;

    [Option("-tc")] private Color TextColor { get; set; } = Color.Black;

    [Option("-ff")] private string FontFamily { get; set; } = "Arial";

    [Option("-fs")] private int FontSize { get; set; } = 50;

    [Option("-if")] private ImageFormat SaveImageFormat { get; set; } = ImageFormat.Png;

    [Option("-ef")] private string ExcludedWordsFile { get; set; } = "ExcludedWords.txt";

    [Option("-rp")]
    private HashSet<string> RemovedPartsOfSpeech { get; set; } = new()
        { "ADVPRO", "APRO", "INTJ", "CONJ", "PART", "PR", "SPRO" };


    private void OnExecute()
    {
        var services = new ServiceCollection();
        services.AddTransient<Font>(x => new Font(FontFamily, FontSize));
        services.AddTransient<Palette>(x => new Palette(TextColor, BackgroundColor));
        services.AddTransient<IPointGenerator, SpiralPointGenerator>();
        services.AddTransient<IDullWordChecker>(x =>
            new MystemDullWordChecker(RemovedPartsOfSpeech, ExcludedWordsFile));
        services.AddTransient<IInterestingWordsParser, MystemWordsParser>();
        services.AddTransient<IRectangleLayouter, RectangleLayouter>();
        services.AddTransient<LayoutDrawer>();

        using var provider = services.BuildServiceProvider();

        var layoutDrawer = provider.GetRequiredService<LayoutDrawer>();
        layoutDrawer
            .CreateLayoutImageFromFile(InputFilePath, new Size(ImageWidth, ImageHeight))
            .SaveImage(OutputFilePath, SaveImageFormat);
    }
}