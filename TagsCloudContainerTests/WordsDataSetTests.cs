using FluentAssertions;
using TagsCloudContainer;

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

        var actual = new WordsDataSet(testString).CreateFrequencyDict();

        actual.Should().Equal(expected);
    }
}