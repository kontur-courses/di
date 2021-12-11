using FluentAssertions;
using NUnit.Framework;
using TagCloud.Analyzers;

namespace TagCloudTests
{
    public class WordsToLowerConverterTests
    {
        private IWordConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new WordsToLowerConverter();
        }

        [Test]
        public void Analyze_ShouldSkipBoringWords()
        {
            var text = new[] { "I", "MET", "yOu", "A", "lOnG", "TiMe", "aGO" };

            var analyzedWords = converter.Convert(text);

            analyzedWords.Should().BeEquivalentTo("i", "met", "you", "a", "long", "time", "ago");
        }
    }
}
