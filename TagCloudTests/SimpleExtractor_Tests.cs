using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WordCloudImageGenerator.Parsing.Extractors;

namespace TagCloudTests
{
    [TestFixture]
    class SimpleExtractor_Tests
    {

        private SimpleExtractor Extractor;
        [SetUp]
        public void SetUp()
        {
            Extractor = new SimpleExtractor();
        }

        [Test]
        public void GetWords()
        {
            var text = "good word";
            var words = Extractor.GetWords(text);

            IEnumerable<string> expectedWords = new List<string>()
            {
                "good",
                "word"
            };

            CollectionAssert.AreEqual(expectedWords, words);
        }

        [Test]
        public void GetWords_NotReturnsNumbers()
        {
            var text = "word1233";
            var words = Extractor.GetWords(text);

            IEnumerable<string> expectedWords = new List<string>()
            {
                "word",
            };

            CollectionAssert.AreEqual(expectedWords, words);
        }

        [Test]
        public void GetWords_NotReturnsChars()
        {
            var text = "word ,*&777&?¹!";
            var words = Extractor.GetWords(text);

            IEnumerable<string> expectedWords = new List<string>()
            {
                "word"
            };

            CollectionAssert.AreEqual(expectedWords, words);
        }
    }
}