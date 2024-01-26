using McMaster.Extensions.CommandLineUtils;
using SixLabors.ImageSharp;
using System.ComponentModel.DataAnnotations;
using TagsCloud.Entities;
using TagsCloudVisualization;

// ReSharper disable MemberCanBePrivate.Global

namespace TagsCloud;

public class Program
{
    [Option]
    public CaseType WordsCase { get; } = CaseType.Lower;

    [Option]
    public bool Infinitive { get; }

    [Option("-wi|--width"), Required]
    public int? Width { get; }

    [Option("-he|--height"), Required]
    public int? Height { get; }

    [Option(CommandOptionType.MultipleValue)]
    public HashSet<string> TextParts { get; } = new();

    [Option(CommandOptionType.MultipleValue)]
    public HashSet<string> Excluded { get; } = new();

    [Option(CommandOptionType.MultipleValue), Required]
    public HashSet<string> Colors { get; } = new();

    [Option]
    public ColoringStrategy Strategy { get; } = ColoringStrategy.AllRandom;

    [Option]
    public float DistanceDelta { get; } = 0.1f;

    [Option]
    public float AngleDelta { get; } = (float)Math.PI / 180;

    [Option, Required]
    public string BackgroundColor { get; set; }

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
        var spiral = new SpiralPointGenerator(DistanceDelta, AngleDelta);
        var layout = new Layout(spiral, new PointF((float)Width!.Value / 2, (float)Height!.Value / 2));

        var colors = Colors!.Select(color => Color.ParseHex(color)).ToArray();

        var options = new OptionsBuilder()
                      .SetColorizer(colors, Strategy)
                      .SetWordsCase(WordsCase)
                      .SetCastPolitics(Infinitive)
                      .SetExcludedWords(Excluded)
                      .SetImportantLanguageParts(TextParts)
                      .SetFontFamily(FontPath)
                      .SetImageSettings(Color.ParseHex(BackgroundColor), new Size(Width.Value, Height.Value))
                      .SetLayout(layout)
                      .Build();

        var facade = new TagCloudFacade(options);
        var tagList = facade.GenerateCloudTagList(InputFile);

        facade.GenerateTagCloudImage(tagList, OutputFile);

        Console.WriteLine("TagsCloud image saved to " + OutputFile);
    }
}