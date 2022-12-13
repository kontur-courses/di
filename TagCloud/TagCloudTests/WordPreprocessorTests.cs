using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
public class WordPreprocessorTests
{
    [Test]
    public void Process_MakeLowerCased()
    {
        var words = new List<string> { "APPLE", "Blueberries", "ApplE", "wAtermelOn", "blueberries", "lemoN", "lEMOn" };

        var wordPreprocessor = new WordPreprocessor();

        wordPreprocessor
            .Process(words)
            .Should()
            .BeEquivalentTo(words.Select(word => word.ToLower()));
    }
}