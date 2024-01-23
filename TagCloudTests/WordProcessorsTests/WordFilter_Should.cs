using TagCloud.Filter;

namespace TagCloudTests.WordProcessorsTests;

[TestFixture]
public class WordFilter_Should
{
    private IFilter sut;
    private List<string> words = new() { "aba", "cabc", "testword", "zaz" };

    [SetUp]
    public void SetUp()
    {
        sut = new WordFilter();
    }

    [Test]
    public void FilterWordList_ByGivenFilter()
    {
        sut = sut.UsingFilter(w => w.Length > 3);

        var result = sut.FilterWords(words);

        result.Should().Contain(new List<string> { "cabc", "testword" });
    }

    [Test]
    public void ReturnSameWords_WhenFilterIsUseless()
    {
        sut = sut.UsingFilter(w => true);

        var result = sut.FilterWords(words);

        result.Should().BeEquivalentTo(words);
    }

    [Test]
    public void ShouldReturnEmpty_WhenFilterFiltersAllWords()
    {
        sut = sut.UsingFilter(w => false);

        var result = sut.FilterWords(words);

        result.Should().BeEmpty();
    }
}