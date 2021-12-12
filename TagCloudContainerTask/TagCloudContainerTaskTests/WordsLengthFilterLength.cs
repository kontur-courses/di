using System;
using System.Collections.Generic;
using App.Implementation.Words.Filters;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTaskTests
{
    public class WordsLengthFilterLength
    {
        [TestCase(-1)]
        [TestCase(0)]
        public void Ctor_ShouldThrowWhenIncorrectLength(int minLength)
        {
            Action act = () => new WordsLengthFilter(minLength);

            act.Should().Throw<ArgumentException>();
        }

        public void Should_ApplyFilterToWords()
        {
            var filter = new WordsLengthFilter(4);

            var words = new List<string>
            {
                "Новости",
                "Погода",
                "Или",
                "как",
                "а"
            };

            var filteredWords = filter.FilterWords(words);

            filteredWords.Should().HaveCount(2);
        }
    }
}