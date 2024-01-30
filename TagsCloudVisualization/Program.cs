using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudVisualization;

public class Program
{
    public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    [Argument(0, Description = "Path to txt file with words")]
    [Required(ErrorMessage = "Expected to get path to file with words as first positional argument." +
                             "\nExample: C:\\PathTo\\File.txt\nOr relative to exe: PathTo\\File.txt")]
    public string InputFilePath { get; set; }

    [Argument(1, Description = "Path to output file")]
    [Required(ErrorMessage = "Expected to get output file path as second positional argument." +
                             "\nExample: C:\\PathTo\\File\nOr relative to exe: PathTo\\File")]
    public string OutputFilePath { get; set; }

    [Option("-w", Description = "Image width in pixels")]
    private int ImageWidth { get; set; } = 1000;

    [Option("-h", Description = "Image height in pixels")]
    private int ImageHeight { get; set; } = 1000;

    [Option("-bc", Description = "Image background color from KnownColor enum")]
    private Color BackgroundColor { get; set; } = Color.Wheat;

    [Option("-tc", Description = "Image words colors sequence array from KnownColor enum. " +
                                 "Can be set multiple times for sequence. Example: -tc black -tc white")]
    private Color[] TextColor { get; set; } = { Color.Black };

    [Option("-ff", Description = "Font used for words")]
    private string FontFamily { get; set; } = "Arial";

    [Option("-fs", Description = "Max font size in em")]
    private int FontSize { get; set; } = 50;

    [Option("-mfs", Description = "Min font size in em")]
    private int MinimalFontSize { get; set; } = 0;

    [Option("-img", Description = "Output image format. Choosen from ImageFormat")]
    private ImageFormat SaveImageFormat { get; set; } = ImageFormat.Png;

    [Option("-ef", Description = "Txt file with words to exclude. 1 word in line. Words must be lexems.")]
    private string ExcludedWordsFile { get; set; }

    [Option("-rp", Description = "Parts of speech abbreviations that are excluded from parsed words. " +
                                 "More info here https://yandex.ru/dev/mystem/doc/ru/grammemes-values")]
    private HashSet<string> RemovedPartsOfSpeech { get; set; } = new()
        { "ADVPRO", "APRO", "INTJ", "CONJ", "PART", "PR", "SPRO" };

    [Option("-alg", Description = "Choose algorithm to generate tag cloud. Available: Spiral, Square")]
    private Algorithm Algorithm { get; set; } = Algorithm.Spiral;


    private void OnExecute()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new TagLayoutSettings(Algorithm, RemovedPartsOfSpeech, ExcludedWordsFile));
        services.AddScoped<Font>(x => new Font(FontFamily, FontSize));
        services.AddScoped<IPalette>(x => new Palette(TextColor, BackgroundColor));
        services.AddScoped<IPointGenerator, LissajousCurvePointGenerator>();
        services.AddScoped<IPointGenerator, SpiralPointGenerator>();
        services.AddScoped<IDullWordChecker, MystemDullWordChecker>();
        services.AddScoped<IInterestingWordsParser, MystemWordsParser>();
        services.AddScoped<IRectangleLayouter, RectangleLayouter>();
        services.AddScoped<LayoutDrawer>();

        using var provider = services.BuildServiceProvider();

        var layoutDrawer = provider.GetRequiredService<LayoutDrawer>();
        try
        {
            layoutDrawer
                .CreateLayoutImageFromFile(InputFilePath, new Size(ImageWidth, ImageHeight), MinimalFontSize)
                .SaveImage(OutputFilePath, SaveImageFormat);
        }
        catch (Exception ex)
        {
            if (ex is FileNotFoundException or DirectoryNotFoundException)
            {
                Console.WriteLine(ex.Message);
                if (!Path.IsPathRooted(InputFilePath))
                    Console.WriteLine("Relative paths are searched realative to .exe file. " +
                                      "Try giving an absolute path.");
            }
            else
            {
                throw;
            }
        }
    }
}