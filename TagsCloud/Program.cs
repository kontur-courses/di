using McMaster.Extensions.CommandLineUtils;
using SixLabors.ImageSharp;
using System.ComponentModel.DataAnnotations;
using TagsCloud.Builders;
using TagsCloud.Entities;

// ReSharper disable MemberCanBePrivate.Global

namespace TagsCloud;

public class Program
{
    [Option]
    public CaseType WordsCase { get; }

    [Option]
    public bool Infinitive { get; }

    [Option("-mafs|--max-font-size")]
    public int MaxFontSize { get; } = 100;

    [Option("-mifs|--min-font-size")]
    public int MinFontSize { get; } = 30;

    [Option("-wi|--width"), Required]
    public int? Width { get; }

    [Option("-he|--height"), Required]
    public int? Height { get; }

    [Option(CommandOptionType.MultipleValue)]
    public IEnumerable<string> TextParts { get; } = Array.Empty<string>();

    [Option(CommandOptionType.MultipleValue)]
    public IEnumerable<string> Excluded { get; } = Array.Empty<string>();

    [Option(CommandOptionType.MultipleValue)]
    public HashSet<string> Colors { get; } = new();

    [Option]
    public bool Russian { get; } = false;

    [Option("-so|--sort")]
    public SortType Sort { get; } = SortType.Preserve;

    [Option("-me|--measurer")]
    public MeasurerType MeasurerType { get; } = MeasurerType.Linear;

    [Option]
    public ColoringStrategy Strategy { get; } = ColoringStrategy.AllRandom;

    [Option]
    public float DistanceDelta { get; } = 0.1f;

    [Option]
    public float AngleDelta { get; } = (float)Math.PI / 180;

    [Option]
    public ImageFormat OutputFormat { get; } = ImageFormat.Png;

    [Option]
    public PointGeneratorType Generator { get; } = PointGeneratorType.Spiral;

    [Option]
    public string BackgroundColor { get; } = string.Empty;

    [Option]
    public string FontPath { get; } = string.Empty;

    [Argument(0), Required]
    public string InputFile { get; }

    [Argument(1), Required]
    public string OutputFile { get; }

    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    // ReSharper disable once UnusedMember.Local
    private void OnExecute()
    {
        var inputOptions = new InputOptionsBuilder()
                           .SetWordsCase(WordsCase)
                           .SetCastPolitics(Infinitive)
                           .SetExcludedWords(Excluded)
                           .SetLanguageParts(TextParts)
                           .SetLanguagePolitics(Russian)
                           .BuildOptions();

        var cloudOptions = new CloudOptionsBuilder()
                           .SetColors(Colors)
                           .SetLayout(
                               Generator,
                               new PointF((float)Width!.Value / 2, (float)Height!.Value / 2),
                               DistanceDelta,
                               AngleDelta)
                           .SetColoringStrategy(Strategy)
                           .SetMeasurerType(MeasurerType)
                           .SetFontFamily(FontPath)
                           .SetSortingType(Sort)
                           .SetFontSizeBounds(MinFontSize, MaxFontSize)
                           .BuildOptions();

        var outputOptions = new OutputOptionsBuilder()
                            .SetImageFormat(OutputFormat)
                            .SetImageSize(new Size(Width!.Value, Height!.Value))
                            .SetImageBackgroundColor(BackgroundColor)
                            .BuildOptions();

        var engine = new TagCloudEngine(inputOptions, cloudOptions, outputOptions);
        engine.GenerateTagCloud(InputFile, OutputFile);

        Console.WriteLine("Tag cloud image saved to file " + OutputFile);
    }
}