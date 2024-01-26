using FluentAssertions;
using TagsCloudContainer.utility;

namespace TagsCloudContainerTests;

[TestFixture]
public class WordsDataSetTests
{
    [Test]
    public void FreqDict_CorrectWordCount()
    {
        const string testString = "One, Two, Three, Two, Three, Three";

        var expected = new Dictionary<string, int>
        {
            { "One", 1 },
            { "Two", 2 },
            { "Three", 3 }
        }.Select(kv => (kv.Key, kv.Value));

        var actual = new WordDataSet(new StringTextHandler(testString)).CreateFrequencyDict();

        actual.Should().Equal(expected);
    }

    private class StringTextHandler(string text) : ITextHandler
    {
        public string ReadText()
        {
            return text;
        }
    }
}