using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.WordsParsing.WordsReading;

namespace TagCloud.Tests.TextReaders
{
    [TestFixture]
    public class TxtWordsReader_Should : AbstractWordsReader_Should<TxtWordsReader>
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            baseDir = baseDir + @"txt\";
            reader = new TxtWordsReader();
        }

        [Test]
        public void ReadOneWord_WhenFileHasOnlyOneWord()
        {
            var expectedRes = new List<string> {"word"};
            var res = reader.ReadFrom(baseDir + "one_word.txt");
            res.Should().BeEquivalentTo(expectedRes);
        }

        [Test]
        public void ReadFewWords_WhenFileHasFewWords()
        {
            var expectedRes = new List<string> { "Hello", "world", "Heh" };
            var res = reader.ReadFrom(baseDir + "few_words.txt");
            res.Should().BeEquivalentTo(expectedRes);
        }

        [Test]
        public void ReadLastWord_WhenNoEscapeCharInTheEnd()
        {
            var expectedRes = new List<string> { "word" };
            var res = reader.ReadFrom(baseDir + "one_word_without_escape_char_in_the_end.txt");
            res.Should().BeEquivalentTo(expectedRes);
        }

        [Test]
        public void DivideWords_ByAnyNonLetterOrNonDigitCharacter()
        {
            var expectedRes = "qwertyuiopasdfghjklzxcvbnm1234567890".Select(c => c.ToString()).ToList();
            var res = reader.ReadFrom(baseDir + "words_with_different_delimiters.txt");
            res.Should().BeEquivalentTo(expectedRes);
        }
    }
}