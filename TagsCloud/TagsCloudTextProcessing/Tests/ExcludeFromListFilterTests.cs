using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextProcessing.Filters;

namespace TagsCloudTextProcessing.Tests
{
    [TestFixture]
    public class ExcludeFromListFilterTests
    {
        [Test]
        public void ExcludeWords_Should_ExcludeWords()
        {
            var excludedWords = new[] {"exclude", "this", "words"};
            var inputWords = new[] {"exclude", "tag", "this", "words", "cloud"};

            var wordsAfterExclusion = new ExcludeFromListFilter(excludedWords).Filter(inputWords);

            wordsAfterExclusion.Should().NotContain(excludedWords);
        }

        [Test]
        public void ExcludeWords_Should_NotExcludeWords_When_NoWordsToExclude()
        {
            var excludedWords = new string[0];
            var inputWords = new[] {"should", "stay", "the", "same"};

            var wordsAfterExclusion = new ExcludeFromListFilter(excludedWords).Filter(inputWords);

            wordsAfterExclusion.Should().BeEquivalentTo(inputWords);
        }

        [Test]
        public void ExcludeWords_Should_ExcludeOnlySpecifiedWords()
        {
            var excludedWords = new[] {"a", "b", "c"};
            var inputWords = new[] {"f", "a", "ab", "a b", "c a", "aba", "c", "a b a"};

            var wordsAfterExclusion = new ExcludeFromListFilter(excludedWords).Filter(inputWords);

            wordsAfterExclusion.Should().BeEquivalentTo("f", "ab", "a b", "c a", "aba", "a b a");
        }
        
        [Test]
        public void ExcludeWords_Should_ExcludeWords_NotCaseSensitive()
        {
            var excludedWords = new[] {"eXclude", "tHis", "Words"};
            var inputWords = new[] {"excludE", "tag", "thiS", "WORDS", "cloud"};

            var wordsAfterExclusion = new ExcludeFromListFilter(excludedWords).Filter(inputWords);

            wordsAfterExclusion.Should().NotContain(excludedWords);
        }
    }
}