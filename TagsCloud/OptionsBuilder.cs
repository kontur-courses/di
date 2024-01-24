using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Reflection;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloud.Helpers;
using TagsCloudVisualization;

namespace TagsCloud;

public class OptionsBuilder
{
    private readonly string[] languageParts =
    {
        "A", "ADV", "ADVPRO", "ANUM",
        "APRO", "COM", "CONJ", "INTJ",
        "NUM", "PART", "PR",
        "S", "SPRO", "V"
    };

    private readonly ITagCloudOptions options;

    public OptionsBuilder()
    {
        options = new TagCloudOptions();
    }

    public OptionsBuilder SetWordsCase(CaseType wordsCase)
    {
        options.WordsCase = wordsCase;
        return this;
    }

    public OptionsBuilder SetCastPolitics(bool castWordsToInfinitive)
    {
        options.CastWordsToInfinitive = castWordsToInfinitive;
        return this;
    }

    public OptionsBuilder SetImportantLanguageParts(string[] parts)
    {
        if (parts.Any(part => !languageParts.Contains(part)))
            throw new ArgumentException("Unknown language part!");

        options.ImportantLanguageParts = parts;
        return this;
    }

    public OptionsBuilder SetExcludedWords(string[] excludedWords)
    {
        options.ExcludedWords = excludedWords;
        return this;
    }

    public OptionsBuilder SetImageSettings(Color backgroundColor, Size imageSize)
    {
        options.BackgroundColor = backgroundColor;
        options.ImageSize = imageSize;
        return this;
    }

    public OptionsBuilder SetLayout(ILayout layout)
    {
        options.Layout = layout;
        return this;
    }

    public OptionsBuilder SetColorizer(Color[] colors, ColoringStrategy strategy)
    {
        var colorizer = ColorizerHelper.GetAppropriateColorizer(colors, strategy);

        if (colorizer == null)
            throw new ArgumentException("Unknown colorizer type! Candidates are: OneVsRest, AllRandom");

        options.Colorizer = colorizer;
        return this;
    }

    public OptionsBuilder SetFontFamily(string? fontPath)
    {
        var collection = new FontCollection();

        if (fontPath != null && File.Exists(fontPath))
        {
            collection.Add(fontPath);
        }
        else
        {
            using var fontStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("TagsCloud.Fonts.Vollkorn-SemiBold.ttf");

            collection.Add(fontStream!);
        }

        options.FontFamily = collection.Families.First();
        return this;
    }

    public ITagCloudOptions Build()
    {
        return options;
    }
}