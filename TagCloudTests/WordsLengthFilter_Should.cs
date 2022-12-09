using TagCloudCreatorExtensions.WordsFilters;
using TagCloudCreatorExtensions.WordsFilters.Settings;

namespace TagCloudTests;

[TestFixture]
public class WordsLengthFilter_Should
{
    private WordsLengthFilter _lengthFilter = null!;
    private IWordsLengthFilterSettings _settings = null!;

    [SetUp]
    public void SetUp()
    {
        _settings = new FiltersSettings
        {
            MinWordLength = 0,
            MaxWordLength = int.MaxValue
        };
        _lengthFilter = new WordsLengthFilter(_settings);
    }

    [Test]
    public void DontSkipWord_IfCorrect()
    {
        _lengthFilter.FilterWords(new[] {"слово"})
            .Should().ContainSingle()
            .Which.Should().Be("слово");
    }

    [Test]
    public void SkipWord_IfShorterThanMin()
    {
        _settings.MinWordLength = 3;
        _lengthFilter.FilterWords(new[] {"я"}).Should().BeEmpty();
    }

    [Test]
    public void SkipWord_IfLongerThanMax()
    {
        _settings.MaxWordLength = 3;
        _lengthFilter.FilterWords(new[] {"слово"}).Should().BeEmpty();
    }

    [Test]
    public void SkipOnlyIncorrectWords_Successfully()
    {
        _settings.MinWordLength = 3;
        _settings.MaxWordLength = 5;
        _lengthFilter.FilterWords(new[] {"слово", "я", "длинный"})
            .Should().ContainSingle()
            .Which.Should().Be("слово");
    }
}