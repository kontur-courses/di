using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextHandlers.Parser;

namespace TagCloud.Tests
{
    [TestFixture]
    public class WordsParserTests
    {
        private static string[] words = { "abc", "abcd", "1234", "test" };
        private static string filename = "TestWords.txt";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            File.WriteAllText(filename, string.Join(Environment.NewLine, words));
        }

        [Test]
        public void GetWords_ShouldReturnAllWordsFromFile()
        {
            var sut = new WordsParser();

            var actualWords = sut.GetWords(filename);

            actualWords.Should().BeEquivalentTo(words);
        }
    }
}