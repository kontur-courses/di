using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Tools;

namespace TagsCloudGeneratorTests.ToolsTests
{
    public class WordsParserTests
    {
        private IWordsParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new TagsCloudGenerator.Tools.WordsParser();
        }

        [Test]
        public void Parse_SimpleTextWithOutSpace_ShouldReturnAllSubstringSeparetedSpace()
        {
            var text = "first third sixth fourth";
            var expected = new List<string>{"first", "third", "sixth", "fourth"};

            var actual = parser.Parse(text);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Parse_TextWithPunctuationMarks_ShouldReturnWordsWithoutPunctuationMarks()
        {
            var text = "first, third! sixth? fourth. second: seventh; " +
                       "- eleventh \'hundredth\' \"fifteenth\" (fifth eighth)";
            var expected = new List<string>
            {
                "first", 
                "third", 
                "sixth",
                "fourth", 
                "second", 
                "seventh", 
                "eleventh", 
                "hundredth", 
                "fifteenth", 
                "fifth", 
                "eighth"
            };

            var actual = parser.Parse(text);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Parse_WordContainsApostrophe_ShouldReturnRightWordPart()
        {
            var text = "first's he's we're I've I'm didn't don't, haven't hasn't";
            var expected = new List<string>
            {
                "first",
                "he",
                "we",
                "I",
                "I",
                "did",
                "do",
                "have",
                "has"
            };

            var actual = parser.Parse(text);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Parse_TextContainsEscapeSymbols_ShouldReturnWordsWithoutEscapeSymbols()
        {
            var text = "\tfirst\t \rthird\r  \vsixth\v \ffourth\f \nsecond\n";
            var expected = new List<string> { "first", "third", "sixth", "fourth", "second" };

            var actual = parser.Parse(text);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}