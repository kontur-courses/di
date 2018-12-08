using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudPreprocessor;

namespace TagsCloudPreprocessorTests
{
    [TestFixture]
    public class SimplePreprocessor_Should
    {
        private IEnumerable<string> Preprocess(IEnumerable<string> words)
        {
            var excluder = new XmlWordExcluder();
            var preprocessor = new SimpleWordsValidator(excluder);

            return preprocessor.GetValidWords(words);
        }

        [Test]
        public void ExcludeForbiddenWords()
        {
            var excluder = new XmlWordExcluder();
            var preprocessor = new SimpleWordsValidator(excluder);
            var excludedWords = excluder.GetExcludedWords();

            preprocessor.GetValidWords(excludedWords).Should().BeEmpty();
        }

        [Test]
        public void ReturnsWordInFrequencyOrder()
        {
            var words = new List<string> { "a", "a", "a", "b", "c", "c" };
            var expected = new List<string> { "a", "c", "b" };
            Preprocess(words).ShouldAllBeEquivalentTo(expected);
        }
    }
}
