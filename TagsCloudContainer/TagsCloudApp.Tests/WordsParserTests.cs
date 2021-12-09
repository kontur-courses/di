using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp.WordsLoading;

namespace TagsCloud.Tests
{
    public class WordsParserTests
    {
        private WordsParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new WordsParser();
        }

        [TestCase(",")]
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("\r\n")]
        public void Parse_WithSeparatedWords_ReturnAllWords(string separator)
        {
            var words = new List<string> {"aaa", "bbb", "ccc", "ddd"};
            var input = string.Join(separator, words);

            parser.Parse(input)
                .Should().BeEquivalentTo(words);
        }

        [Test]
        public void Parse_ReturnWords_WithHyphen()
        {
            var input = "white-black";

            parser.Parse(input)
                .Should().BeEquivalentTo(input);
        }
    }
}