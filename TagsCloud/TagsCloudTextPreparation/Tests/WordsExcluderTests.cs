using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextPreparation.Excluders;

namespace TagsCloudTextPreparation.Tests
{
    [TestFixture]
    public class WordsExcluderTests
    {
        [Test]
        public void ExcludeWords_Should_ExcludeWords()
        {
            var excludedWords = new[] {"exclude", "this", "words"};
            var inputWords = new[] {"exclude", "tag", "this", "words", "cloud"};

            var wordsAfterExclusion = new WordsExcluder().ExcludeWords(inputWords, excludedWords);

            wordsAfterExclusion.Should().NotContain(excludedWords);
        }

        [Test]
        public void ExcludeWords_Should_ExcludeOnlySpecifiedWords()
        {
            var excludedWords = new[] {"a", "b", "c"};
            var inputWords = new[] {"f", "a", "ab", "a b", "c a", "aba", "c", "a b a"};

            var wordsAfterExclusion = new WordsExcluder().ExcludeWords(inputWords, excludedWords);
            
            wordsAfterExclusion.Should().BeEquivalentTo("f", "ab", "a b", "c a", "aba", "a b a");
        }
    }
}