using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    class BlackListWordsFilterTests
    {
        private readonly WordNormalizer normalizer = new WordNormalizer();
        [TestCase(new[] { "a", "b", "c" }, new string[0], new string[0])]
        [TestCase(new[] { "a", "b", "c" }, new[] { "d", "d", "a", "a", "a", "c", "f" }, new[] { "d", "d", "f" })]
        [TestCase(new [] { "a", "b", "c", "d", "e", "f" }, new[] { "d", "d", "a", "a", "a", "c", "f" }, new string[0])]
        public void BlacklistWordsFilter_TestCases(IEnumerable<string> blackList, IEnumerable<string> words, IEnumerable<string> expectedResult)
        {
            var filter = new BlackListWordsFilter(blackList.ToHashSet(), normalizer);
            var filtredWords = filter.FilterWords(words);
            filtredWords.Should().BeEquivalentTo(expectedResult);
        }
    }
}
