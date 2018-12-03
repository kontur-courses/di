using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Data;
using TagCloud.Parser;
using TagCloud.RectanglesLayouter;
using TagCloud.RectanglesLayouter.PointsGenerator;
using TagCloud.WordsLayouter;

namespace TagCloudTests
{
    [TestFixture]
    public class WordsParserTests
    {
        private WordsParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new WordsParser();
        }

        [Test]
        public void Parse_ShouldReturnWordsWithOccurrences()
        {
            var words = parser.Parse(new[] { "a", "a", "b", "b", "b", "c" }, new HashSet<string>());
            words.Should().BeEquivalentTo(new WordInfo("b", 3), new WordInfo("a", 2), new WordInfo("c", 1));
        }

        [Test]
        public void Parse_ShouldReturnWordsInOccurrencesDescendingOrder()
        {
            var words = parser.Parse(new[] { "a", "a", "b", "b", "b", "c" }, new HashSet<string>());
            words.Should().BeInDescendingOrder(info => info.Occurrences);
        }

        [Test]
        public void Parse_ShouldReturnWordsInLowerCase()
        {
            var words = parser.Parse(new[] { "a", "a", "b", "b", "b", "c" }, new HashSet<string>());
            foreach (var wordInfo in words)
                wordInfo.Word.Should().Be(wordInfo.Word.ToLower());
        }

        [Test]
        public void Parse_ShouldReturnAllWords_WithoutBoringWords()
        {
            var words = parser.Parse(new[] { "a", "a", "b", "b", "b", "c" }, new HashSet<string>());
            words.Select(info => info.Word).Should().Contain("a", "b", "c");
        }

        [Test]
        public void Parse_ShouldNotReturnBoringWords()
        {
            var words = parser.Parse(new[] { "a", "a", "b", "b", "b", "c" }, new HashSet<string> { "a" });
            words.Select(info => info.Word).Should().NotContain("a");
        }

        [Test]
        public void Parse_ShouldReturnWordsWithoutRepetitions()
        {
            var words = parser.Parse(new[] { "a", "a", "b", "b", "b", "c" }, new HashSet<string>());
            words.Select(info => info.Word).Should().OnlyHaveUniqueItems();
        }
    }
}