using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class WordsFromFileTests
    {
        private readonly List<string> expectedWords = new List<string>
        {
            "Neural",
            "networks",
            "are",
            "not",
            "the",
            "only",
            "machine",
            "learning",
            "frameworks"
        };

        [Test]
        public void GetWords_EmptyFile_EmptyList()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Empty.txt");
            var wordsFromFile = new WordsFromFile(path);
            var words = wordsFromFile.GetWords();
            words.Should().BeEmpty();
        }

        [Test]
        public void GetWords_GivePath_ReturnList()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Text.txt");
            var wordsFromFile = new WordsFromFile(path);
            var words = wordsFromFile.GetWords();
            words.Should().BeEquivalentTo(expectedWords);
        }
    }
}