using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextProvider;

namespace TagCloud_Should
{
    public class TextParser_Should
    {
        private TextParser textParser;
        private UnitTestsTextProvider textProvider = new UnitTestsTextProvider();

        [SetUp]
        public void SetUp()
        {
            textParser = new TextParser(textProvider);
        }

        [Test]
        public void TextParser_ShouldParseComplicatedLines()
        {
            textParser.ParseText().Should().BeEquivalentTo(new List<string>
            {
                "word1", "word2", "", "word", "", "than", "more", "", "", "word", "word1", "", "", "word", "word1",
                "word1", "word1", "word2", "word2", "word2", "blacklistword", "blacklistword", "word3", "blacklistword",
                "", "word", "", "the", "", "", "word", "", "am", "i", "word", "", "is", "it", "unit", "test", "", "are",
                "u", "mad", "", "", "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n"
            });
        }

        [Test]
        public void ShouldParseLinesWithSpaces()
        {
            textParser.ParseAllLines(textProvider.GetLineWithSpaces()).Should().BeEquivalentTo(new List<string>
            {
                "sentence", "with", "spaces", "must", "be", "parsed", "correctly"
            });
        }

        [Test]
        public void ShouldParseLinesWithPunctuationSigns()
        {
            textParser.ParseAllLines(textProvider.GetLineWithPunctuationSigns()).Should().BeEquivalentTo(new List<string>
            {
                "sentence", "", "with", "", "punctuation", "signs", "", "must", "", "be", "parsed", "correctly", "", "isn", "t", "it", ""
            });
        }
    }
}