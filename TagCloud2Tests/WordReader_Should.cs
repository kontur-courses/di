using System;
using NUnit;
using FluentAssertions;
using NUnit.Framework;
using TagCloud2;

namespace TagCloud2Tests
{
    public class LinesWordReader_Should
    {
        private LinesWordReader wr = new();

        [Test]
        public void GetWords_OnOneWord_ShouldReturnThisWordInArray()
        {
            var input = "Слово";
            var expected = new string[] { "Слово" };
            wr.GetUniqueLowercaseWords(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWords_OnMultipleWords_ShouldReturnWordsInArray()
        {
            var input = "Слово\r\nДело";
            var expected = new string[] { "Слово", "Дело" };
            wr.GetUniqueLowercaseWords(input).Should().BeEquivalentTo(expected);
        }
    }
}
