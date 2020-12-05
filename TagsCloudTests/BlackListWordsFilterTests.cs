using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    internal class BlackListWordsFilterTests
    {
        private readonly WordNormalizer normalizer = new WordNormalizer();

        [TestCase(new[] {"a", "b", "c"}, new string[0], new string[0])]
        [TestCase(new[] {"a", "b", "c"}, new[] {"d", "d", "a", "a", "a", "c", "f"}, new[] {"d", "d", "f"})]
        [TestCase(new[] {"a", "b", "c", "d", "e", "f"}, new[] {"d", "d", "a", "a", "a", "c", "f"}, new string[0])]
        public void BlacklistWordsFilter_ShouldValidateWords_ThatAreNotInBlackList(IEnumerable<string> blackList,
            IEnumerable<string> words, IEnumerable<string> expectedResult)
        {
            var filter = new BlackListWordsFilter(blackList, normalizer);
            var filtredWords = words.Where(word => filter.Validate(word));
            filtredWords.Should().BeEquivalentTo(expectedResult);
        }
    }
}