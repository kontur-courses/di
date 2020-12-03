using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    class WordsAnalizationTests
    {
        [TestCase(new string[0], new string[0], new string[0])]
        [TestCase(new string[0], new [] {"ABC"}, new [] {"abc"})]
        [TestCase(new [] {"ABC"}, new [] {"abc"}, new string[0])]
        [TestCase(new [] {"567"}, new [] {"AbC", "567"}, new [] {"abc"})]
        public void WordsAnalization_TestCases(IEnumerable<string> blackList, IEnumerable<string> words, IEnumerable<string> expectedResult)
        {
            var normalizer = new WordNormalizer();
            var filter = new BlackListWordsFilter(blackList.ToHashSet(), normalizer);
            var result = filter.FilterWords(words.Select(word => normalizer.NormalizeWord(word)));
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
