using TagCloud.WordsPreprocessor;

namespace TagCloudTests.WordProcessorsTests;

[TestFixture]
public class DefaultPreprocessor_Should
{
    private IPreprocessor sut = new DefaultPreprocessor();
    private List<string> words = new() { "Aba", "caBC", "teSTword", "ZAZ", "word" };

    [SetUp]
    public void SetUp()
    {
        sut = new DefaultPreprocessor();
    }

    [Test]
    public void ConvertAllWordsToLowerCase()
    {
        var expected = words.Select(w => w.ToLower());
        var result = sut.HandleWords(words);

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ReturnEmpty_WhenNoWordsGiven()
    {
        var result = sut.HandleWords(new List<string>());

        result.Should().BeEmpty();
    }
}