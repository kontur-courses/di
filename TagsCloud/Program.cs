using McMaster.Extensions.CommandLineUtils;
using SixLabors.ImageSharp;
using System.ComponentModel.DataAnnotations;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud;

public class Program
{
    [Option] public CaseType WordsCase { get; }

    [Option] public bool Infinitive { get; }

    [Option("-wi|--width")] [Required] public int? Width { get; }

    [Option("-he|--height")] [Required] public int? Height { get; }

    [Option(CommandOptionType.MultipleValue)]
    public string[]? TextParts { get; }

    [Option(CommandOptionType.MultipleValue)]
    public string[]? Excluded { get; } = Array.Empty<string>();

    [Option(CommandOptionType.MultipleValue)]
    [Required]
    public string[]? Colors { get; }

    [Option] public ColoringStrategy Strategy { get; } = ColoringStrategy.AllRandom;

    [Option] public float DistanceDelta { get; } = 0.1f;

    [Option] public float AngleDelta { get; } = (float)Math.PI / 180;

    [Option] [Required] public string? BackgroundColor { get; set; }

    [Option] public string? FontPath { get; }

    [Argument(0)] [Required] public string? InputFile { get; }

    [Argument(1)] [Required] public string? OutputFile { get; }

    public static int Main(string[] args)
    {
        return CommandLineApplication.Execute<Program>(args);
    }

    private void OnExecute()
    {
        var spiral = new Spiral(DistanceDelta, AngleDelta);
        var layout = new Layout(spiral, new PointF((float)Width.Value / 2, (float)Height.Value / 2));

        var colors = Colors!
            .Select(color => Color.ParseHex(color))
            .ToArray();

        var options = new OptionsBuilder()
            .SetColorizer(colors, Strategy)
            .SetWordsCase(WordsCase)
            .SetCastPolitics(Infinitive)
            .SetExcludedWords(Excluded!)
            .SetImportantLanguageParts(TextParts!)
            .SetFontFamily(FontPath)
            .SetImageSettings(Color.ParseHex(BackgroundColor!), new Size(Width.Value, Height.Value))
            .SetLayout(layout)
            .Build();

        var facade = new TagCloudFacade(options);

        var tagList = facade.GenerateCloudTagList(InputFile!);
        facade.GenerateTagCloudImage(tagList, OutputFile!);

        Console.WriteLine("TagsCloud image saved to " + OutputFile);
    }
}