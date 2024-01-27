using McMaster.Extensions.CommandLineUtils;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TagsCloud.Entities;
using TagsCloudVisualization;

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
    public HashSet<string> TextParts { get; } = new();

    [Option(CommandOptionType.MultipleValue)]
    public HashSet<string> Excluded { get; } = new();

    [Option(CommandOptionType.MultipleValue)]
    public HashSet<string> Colors { get; } = new();

    [Option]
    public ColoringStrategy Strategy { get; } = ColoringStrategy.AllRandom;

    [Option]
    public float DistanceDelta { get; } = 0.1f;

    [Option]
    public float AngleDelta { get; } = (float)Math.PI / 180;

    [Option]
    public string BackgroundColor { get; set; } = string.Empty;

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

    private void OnExecute()
    {
        var spiralGenerator = new SpiralPointGenerator(DistanceDelta, AngleDelta);
        var center = new PointF((float)Width!.Value / 2, (float)Height!.Value / 2);

        var layout = new Layout(spiralGenerator, center);

        var textOptions = new InputProcessorOptions
        {
            ToInfinitive = Infinitive, WordsCase = WordsCase, ExcludedWords = Excluded, LanguageParts = TextParts
        };

        var cloudOptions = new CloudProcessorOptions
        {
            ColoringStrategy = Strategy,
            Colors = InputParser.ParseTagColors(Colors),
            MinFontSize = MinFontSize,
            MaxFontSize = MaxFontSize,
            Layout = layout,
            FontFamily = LoadFontFamily(FontPath)
        };

        var outputOptions = new OutputProcessorOptions
        {
            BackgroundColor = InputParser.ParseBackgroundColor(BackgroundColor),
            ImageSize = new Size(Width!.Value, Height!.Value),

            // Expansion point here!
            ImageEncoder = new PngEncoder()
        };

        var engine = new TagCloudEngine(textOptions, cloudOptions, outputOptions);
        engine.GenerateTagCloud(InputFile, OutputFile);

        Console.WriteLine("Tag cloud image saved to file " + OutputFile);
    }

    private static FontFamily LoadFontFamily(string fontPath)
    {
        var fontCollection = new FontCollection();

        if (File.Exists(fontPath))
        {
            fontCollection.Add(fontPath);
        }
        else
        {
            const string fontName = nameof(TagsCloud) + ".Fonts.Vollkorn-SemiBold.ttf";
            var fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fontName);
            fontCollection.Add(fontStream!);
        }

        return fontCollection.Families.First();
    }
}