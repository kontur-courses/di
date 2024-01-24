using TagCloud.WordRanker;

namespace TagCloudTests.WordProcessorsTests;

[TestFixture]
public class WordRanker_Should
{
    private IWordRanker sut;
    private List<string> words = new() { "aba", "caba", "aba", "a", "a", "a" };

    [SetUp]
    public void SetUp()
    {
        sut = new WordRankerByFrequency();
    }

    [Test]
    public void CorrectlyRankWords()
    {
        var expected = words.GroupBy(word => word.Trim().ToLowerInvariant()).OrderByDescending(g => g.Count()).ToList()
            .Select(g => ValueTuple.Create(g.Key, g.Count()));
        var result = sut.RankWords(words);

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ReturnEmpty_WhenNowordsGiven()
    {
        var result = sut.RankWords(new List<string>());

        result.Should().BeEmpty();
    }
}