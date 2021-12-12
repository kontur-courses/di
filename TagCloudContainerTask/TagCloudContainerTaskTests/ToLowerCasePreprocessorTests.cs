using System.Collections.Generic;
using System.Linq;
using App.Implementation.Words.Preprocessors;
using App.Infrastructure.Words.Preprocessors;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTaskTests
{
    public class ToLowerCasePreprocessorTests
    {
        private IPreprocessor preprocessor;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            preprocessor = new ToLowerCasePreprocessor();
        }

        [Test]
        public void Should_LeadAllWordsToLowerCase()
        {
            var words = new List<string> { "СлоВа", "В", "разНОм", "РеГИстРе" };
            var expected = words.Select(word => word.ToLower()).ToList();
            var result = preprocessor.Preprocess(words).ToList();

            result.Should().Equal(expected);
        }
    }
}