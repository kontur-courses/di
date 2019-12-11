using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Core.Filters;

namespace TagsCloudGeneratorTests.WordsFilterTests
{
    [TestFixture]
    public class WordsFilterTests
    {
        private readonly IWordsFilter wordsFilter = new WordsFilter(); 
            
        [TestCaseSource(typeof(WordsFilterTestsData),
            nameof(WordsFilterTestsData.TestCases))]
        public void GetFilteredWords_ShouldReturnCorrectFilteredWords(
            List<string> text,
            List<string> expectedResult)
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            var result = wordsFilter.GetFilteredWords(text).ToList();
            result.Should().HaveSameCount(expectedResult);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}