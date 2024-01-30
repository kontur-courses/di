using FluentAssertions;
using TagsCloud;
using TagsCloud.App.Settings;
using TagsCloud.WordAnalyzer;

namespace TagsCloudTests;

public class AnalyzerTests
{
    private WordAnalyzer sut;
    private WordAnalyzerSettings settings;

    [SetUp]
    public void SetUp()
    {
        settings = new WordAnalyzerSettings();
        sut = new WordAnalyzer(settings);
    }

    [Test]
    public void WordAnalyzer_WhenEmptyCollection_ShouldBeEmpty()
    {
        sut.GetFrequencyList(new List<string>()).Should().BeEmpty();
    }

    [Test]
    public void WordAnalyzer_WhenWithSelectedPartSpeech_ShouldBeOnlySelectedPartSpeech()
    {
        var words = new List<string> { "он", "плохой", "человек" };
        settings.SelectedSpeeches = new List<PartSpeech> { PartSpeech.Noun };
        sut.GetFrequencyList(words).Should().BeEquivalentTo(new List<WordInfo> { new((string)"человек", (int)1) });
    }

    [Test]
    public void WordAnalyzer_WhenWithSomeBoringWords_ShouldBeWithoutBoringWords()
    {
        var words = new List<string> { "он", "плохой", "человек" };
        settings.BoringWords = new List<string> { "плохой" };
        sut.GetFrequencyList(words).Should().BeEquivalentTo(new List<WordInfo> { new((string)"он", (int)1), new((string)"человек", (int)1) });
    }

    [Test]
    public void WordAnalyzer_WhenWithExcludedSpeeches_ShouldBeWithoutExcludedSpeeches()
    {
        var words = new List<string> { "он", "плохой", "человек" };
        settings.ExcludedSpeeches = new List<PartSpeech> { PartSpeech.Noun };
        sut.GetFrequencyList(words).Should().BeEquivalentTo(new List<WordInfo> { new((string)"он", (int)1), new((string)"плохой", (int)1) });
    }

    [Test]
    public void WordAnalyzer_WhenDifferentCasesOfWord_ShouldBeWordInInitialForm()
    {
        var words = new List<string>
        {
            "человек",
            "человека",
            "человеку",
            "человека",
            "человеком",
            "человеке",
            "читать",
            "читая",
            "читал",
            "читала",
            "читало",
            "читали"
        };
        sut.GetFrequencyList(words).Should().BeEquivalentTo(new List<WordInfo> { new((string)"человек", (int)6), new((string)"читать", (int)6) });
    }

    [Test]
    public void WordAnalyzer_WhenDifferentCountWords_ShouldBeSortedByDescending()
    {
        var words = new List<string>
        {
            "человек",
            "человека",
            "человеку",
            "человека",
            "человеком",
            "красивый",
            "красивая",
            "красивое",
            "красивые"
        };
        sut.GetFrequencyList(words).Should().BeInDescendingOrder(info => info.Count);
    }

    [Test]
    public void WordAnalyzer_WhenEqualCountWords_ShouldBeSortedByAscendingOrder()
    {
        var words = new List<string>
        {
            "человек",
            "был",
            "красивым"
        };
        sut.GetFrequencyList(words).Should().BeInAscendingOrder(info => info.Word);
    }
}