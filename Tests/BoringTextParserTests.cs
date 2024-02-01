using FluentAssertions;
using TagsCloudPainter.Parser;
using TagsCloudPainter.Settings;

namespace TagsCloudPainterTests;

[TestFixture]
public class BoringTextParserTests
{
    [SetUp]
    public void Setup()
    {
        boringText = $"что{Environment.NewLine}и{Environment.NewLine}в";
        textSettings = new TextSettings { BoringText = boringText };
        boringTextParser = new BoringTextParser(textSettings);
    }

    private ITextSettings textSettings;
    private BoringTextParser boringTextParser;
    private string boringText;

    [Test]
    public void ParseText_ShouldReturnWordsListWithoutBoringWords()
    {
        var boringWords = boringTextParser
            .GetBoringWords(
                boringText)
            .ToHashSet();
        var parsedText = boringTextParser.ParseText("Скучные Слова что в и");
        var isBoringWordsInParsedText = parsedText.Where(boringWords.Contains).Any();
        isBoringWordsInParsedText.Should().BeFalse();
    }

    [Test]
    public void ParseText_ShouldReturnNotEmptyWordsList_WhenPassedNotEmptyText()
    {
        var parsedText = boringTextParser.ParseText("Скучные Слова что в и");
        parsedText.Count.Should().BeGreaterThan(0);
    }

    [Test]
    public void ParseText_ShouldReturnWordsInLowerCase()
    {
        var parsedText = boringTextParser.ParseText("Скучные Слова что в и");
        var isAnyWordNotLowered = parsedText.Any(word => !word.Equals(word, StringComparison.CurrentCultureIgnoreCase));
        isAnyWordNotLowered.Should().BeFalse();
    }

    [Test]
    public void ParseText_ShouldReturnWordsListWithTheSameAmountAsInText()
    {
        var parsedText = boringTextParser.ParseText("Скучные Слова что в и");
        parsedText.Count.Should().Be(2);
    }
}