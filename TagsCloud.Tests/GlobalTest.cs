using FluentAssertions;
using NUnit.Framework;
using SixLabors.ImageSharp;
using TagsCloud.Builders;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Tests;

[TestFixture]
public class GlobalTest
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var textParts = new HashSet<string> { "S" };
        var colors = new HashSet<string> { "#de2114", "#07f727", "#7707f7" };
        var (width, height) = (1920, 1080);

        var inputOptions = new InputOptionsBuilder()
                           .SetWordsCase(CaseType.Upper)
                           .SetCastPolitics(true)
                           .SetExcludedWords(excludedWords)
                           .SetLanguageParts(textParts)
                           .SetLanguagePolitics(true)
                           .BuildOptions();

        var cloudOptions = new CloudOptionsBuilder()
                           .SetColors(colors)
                           .SetLayout(
                               PointGeneratorType.Spiral,
                               new PointF((float)width / 2, (float)height / 2),
                               0.1f,
                               (float)Math.PI / 180)
                           .SetColoringStrategy(ColoringStrategy.AllRandom)
                           .SetMeasurerType(MeasurerType.Linear)
                           .SetFontFamily(string.Empty)
                           .SetSortingType(SortType.Ascending)
                           .SetFontSizeBounds(35, 100)
                           .BuildOptions();

        var outputOptions = new OutputOptionsBuilder()
                            .SetImageFormat(ImageFormat.Jpeg)
                            .SetImageSize(new Size(width, height))
                            .SetImageBackgroundColor("#ffffff")
                            .BuildOptions();

        var engine = new TagCloudEngine(inputOptions, cloudOptions, outputOptions);
        wordGroups = engine.GenerateTagCloud(Path.Join("TestData", "data.txt"), outputPath);
    }

    private readonly HashSet<string> excludedWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "Ноутбук",
        "Камера"
    };

    private const string outputPath = "tagcloud_image.jpeg";

    private readonly HashSet<string> verbs = new(StringComparer.OrdinalIgnoreCase)
    {
        "Программировать",
        "Прыгать",
        "Бегать",
        "Играть"
    };

    private readonly HashSet<string> englishWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "America",
        "Russia",
        "Germany",
        "Apple",
        "TV",
        "Join",
        "Split"
    };

    private HashSet<WordTagGroup> wordGroups;

    [Test]
    public void WordGroups_Should_ContainOnlyRussianWords()
    {
        wordGroups.Should().NotContain(group => englishWords.Contains(group.WordInfo.Text));
    }

    [Test]
    public void WordGroups_ShouldNot_ContainVerbs()
    {
        wordGroups.Should().NotContain(group => verbs.Contains(group.WordInfo.Text));
    }

    [Test]
    public void WordGroups_ShouldNot_ContainExcludedWords()
    {
        wordGroups.Should().NotContain(group => excludedWords.Contains(group.WordInfo.Text));
    }

    [Test]
    public void WordGroupsWords_Should_BeUpperCase()
    {
        foreach (var group in wordGroups)
            AreLettersUpperCase(group.WordInfo.Text).Should().Be(true);
    }

    [Test]
    public void TagCloud_Should_CreateImageFile()
    {
        File.Exists(outputPath).Should().Be(true);
    }

    private static bool AreLettersUpperCase(string word)
    {
        return word.Where(char.IsLetter).All(letter => letter == char.ToUpper(letter));
    }
}