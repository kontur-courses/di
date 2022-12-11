using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloudTest.WordPreprocessor.Infrastructure;

namespace TagCloudTest.WordPreprocessor;

public class DefaultWordsPreprocessorTest
{
    private IWordsPreprocessor? sut;

    [OneTimeSetUp]
    public void StartTests()
    {
        sut = new DefaultWordsPreprocessor(CultureInfo.CurrentCulture);
    }

    [Test]
    public void GetProcessedWords_ShouldReturnOnlyUniqueWordsWithCount()
    {
        var words = new List<string> { "word", "word", "word", "word2" };
        var processedWords = sut!.GetProcessedWords(words, new []{ new NoBoringWords()});
        processedWords.Count.Should().Be(2);
        processedWords.Any(word => word.Value == "word" && word.Count == 3).Should().BeTrue();
    }

    [Test]
    public void GetProcessedWords_ShouldRemoveBoringWords_ByIBoringWords()
    {
        var words = new List<string> { "boring", "boring", "word", "word-2", "boring" };
        var processedWords = sut!.GetProcessedWords(words, new []{new SimpleBoringWordsIdentifier()});
        processedWords.Count.Should().Be(2);
        processedWords.All(word => word.Value != "boring").Should().BeTrue();
    }

    [Test]
    public void GetProcessedWords_ShouldCombine_BoringWordsHandlers()
    {
        var words = new List<string> { "very-boring", "boring", "word", "word-2", "boring", "very-boring" };
        var processedWords = sut!.GetProcessedWords(words,
            new IBoringWords[]{ new SimpleBoringWordsIdentifier(), new SimpleVeryBoringWordsIdentifier() });
        processedWords.Count.Should().Be(2);
        processedWords.All(word => !word.Value.Contains("boring")).Should().BeTrue();
    }
}