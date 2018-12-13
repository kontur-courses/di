using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudPreprocessor;
using TagsCloudPreprocessor.Preprocessors;

namespace TagsCloudPreprocessorTests
{
    [TestFixture]
    public class SimplePreprocessor_Should
    {
        [Test]
        public void ExcludeForbiddenWords()
        {
            var excluder = new XmlWordExcluder();
            var preprocessor = new WordsExcluder(excluder);
            var excludedWords = excluder.GetExcludedWords();

            preprocessor.PreprocessWords(excludedWords.ToList()).Should().BeEmpty();
        }
    }
}
