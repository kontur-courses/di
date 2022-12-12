using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
public class FrequencyDictionaryTests
{
    [Test]
    public void GetWordsFrequency_ContainsAllWord()
    {
        var words = new[] { "Hello", "Yes", "Hello", "B", "No", "Yes" };
        var uniqueWords = words.Distinct();

        var actual = new FrequencyDictionary().GetWordsFrequency(words).Select(x => x.Key).ToArray();

        actual.Should().BeEquivalentTo(uniqueWords);
    }
}