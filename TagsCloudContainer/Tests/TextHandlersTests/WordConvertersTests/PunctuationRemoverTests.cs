using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Core.TextHandler.WordConverters;

namespace TagsCloudContainer.Tests.TextHandlersTests.WordConvertersTests
{
    [TestFixture]
    class PunctuationRemoverTests
    {
        [TestCase(".a", "a", TestName = "WhenDotAtBeginning")]
        [TestCase("a.", "a", TestName = "WhenDotAtEnd")]
        [TestCase(".-]'[,:abcd-';.,", "abcd", TestName = "WhenALotPunctuationAround")]
        public void Convert_ReturnsWordWithoutPunctuationOnSides(string word, string expected)
        {
            var converter = new PunctuationRemover();
            converter.Convert(word).Should().Be(expected);
        }
    }
}