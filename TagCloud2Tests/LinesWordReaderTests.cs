using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud2;

namespace TagCloud2Tests
{
    public class LinesWordReader_Should
    {
        private readonly LinesWordReader wr = new();

        [Test]
        public void GetWords_OnOneWord_ShouldReturnThisWordInArray()
        {
            var input = "Слово";
            var expected = new string[] { "слово" };

            wr.GetUniqueLowercaseWords(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWords_OnMultipleWords_ShouldReturnWordsInArray()
        {
            var input = "Слово"+ Environment.NewLine + "Дело";
            var expected = new string[] { "слово", "дело" };

            wr.GetUniqueLowercaseWords(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWords_OnSameWords_ShouldDistinct()
        {
            var input = string.Format("собака{0}Собака{0}СОБАКА", Environment.NewLine);
            var expected = new string[] { "собака" };

            wr.GetUniqueLowercaseWords(input).Should().BeEquivalentTo(expected);
        }
    }
}
