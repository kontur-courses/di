using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NHunspell;
using NUnit.Framework;
using TagsCloudGenerator.Core.Normalizers;

namespace TagsCloudGeneratorTests.WordsNormalizerTests
{
    [TestFixture]
    public class WordsNormalizerTests
    {
        private readonly IWordsNormalizer wordsNormalizer = new WordsNormalizer();

        private readonly Hunspell hunspell =
            new Hunspell($@"{TestContext.CurrentContext.TestDirectory}\HunspellDictionaries\ru.aff",
                $@"{TestContext.CurrentContext.TestDirectory}\HunspellDictionaries\ru.dic");

        [TestCaseSource(typeof(WordsNormalizerTestsData),
            nameof(WordsNormalizerTestsData.TestCases))]
        public void GetNormalizedWords_ShouldReturnCorrectNormalizedWords(
            List<string> text,
            List<string> expectedResult)
        {
            var result = wordsNormalizer.GetNormalizedWords(text, new HashSet<string>(), hunspell).ToList();
            result.Should().HaveSameCount(expectedResult);
            result.Should().BeEquivalentTo(expectedResult);
        }

        public void GetNormalizedWords_ShouldNotContainBoringWords()
        {
            var result = wordsNormalizer.GetNormalizedWords(
                    new List<string>() {"скука веселье"},
                    new HashSet<string>() {"скука"},
                    hunspell)
                .ToList();
            var expectedResult = new List<string>() {"веселье"};
            result.Should().HaveSameCount(expectedResult);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}