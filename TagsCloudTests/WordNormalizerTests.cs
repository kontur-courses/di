using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    class WordNormalizerTests
    {
        WordNormalizer normalizer = new WordNormalizer();

        [TestCase("Word", "word")]
        [TestCase("SECOND", "second")]
        [TestCase("", "")]
        [TestCase("AbC541", "abc541")]
        [TestCase("kU!@Hg_rug", "ku!@hg_rug")]
        public void WordNormalizer_TestCases(string word, string expectedResult)
        {
            var result = normalizer.NormalizeWord(word);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
