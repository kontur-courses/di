using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
public class SizeByFrequencyTests
{
    [Test]
    public void Wrap_WrappedContainsCorrectText()
    {
        var wordsFrequency = new Dictionary<string, int> { ["Yes"] = 10, ["No"] = 5 };
        var words = wordsFrequency.Select(x => x.Key).ToArray();
        var fontProperties = new FontProperties();

        var wrappedWords = new SizeByFrequency(fontProperties).ResizeAll(wordsFrequency).ToArray();

        wrappedWords.Select(x => x.Text).Should().BeEquivalentTo(words);
    }
}