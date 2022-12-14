using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer;
using TagsCloudContainer.WordsInterfaces;

namespace CloudGeneratorTests;

public class WordsAnalyzer_Should
{
    private List<string> _words;

    [SetUp]
    public void SetUp()
    {
        _words = new List<string> { "я", "привет", "кто", "круг", "прыгать" };
    }

    [Test]
    public void Analyze_WhenDefault_ReturnsAllWordsExceptDefault()
    {
        var actual = AppDIInitializer.Container.GetService<IWordsAnalyzer>()
            .Analyze(_words, new HashSet<string>(), new HashSet<string>());
        var expected = new List<string> { "привет", "круг", "прыгать" };
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Analyze_WhenHasBoringWords_ReturnsWordsWithoutBoring()
    {
        var actual = AppDIInitializer.Container.GetService<IWordsAnalyzer>()
            .Analyze(_words, new HashSet<string> { "привет" }, new HashSet<string>());
        var expected = new List<string> { "круг", "прыгать" };
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void Analyze_WhenHasSpeechPartsToSkip_ReturnsWithoutTheseWords()
    {
        var actual = AppDIInitializer.Container.GetService<IWordsAnalyzer>().Analyze(_words,
            new HashSet<string> { "привет" }, new HashSet<string> { "сущ" });
        var expected = new List<string> { "прыгать" };
        actual.Should().BeEquivalentTo(expected);
    }
}