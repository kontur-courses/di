using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.WordsParsing.WordsReading;

namespace TagCloud.Tests.TextReaders
{
    [TestFixture]
    public class XmlWordsReader_Should : AbstractWordsReader_Should<XmlWordsReader>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            baseDir = baseDir + @"xml\";
            reader = new XmlWordsReader();
        }

        [Test]
        public void ReadOneWord_WhenFileHasOnlyOneWord()
        {
            var expectedRes = new List<string> { "word" };
            var res = reader.ReadFrom(baseDir + "one_word.xml");
            res.Should().BeEquivalentTo(expectedRes);
        }

        [Test]
        public void ReadFewWords_WhenFileHasFewWords()
        {
            var expectedRes = new List<string> { "word1", "word2", "word3" };
            var res = reader.ReadFrom(baseDir + "few_words.xml");
            res.Should().BeEquivalentTo(expectedRes);
        }

        [Test]
        public void ReadZeroWordsWithoutExceptions_WhenFileHasZeroWords()
        {
            var res = reader.ReadFrom(baseDir + "zero_words.xml");
            res.Should().NotBeNull();
            res.Count().Should().Be(0);
        }
    }
}