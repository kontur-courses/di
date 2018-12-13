using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{
    [TestFixture]
    public class TextPreprocessor_Should
    {
        private TextPreprocessor preprocessor;
        private IWordsProvider wordsProvider;
        private IFilter filter;
        private IEnumerable<string> words;
        private IWordsChanger wordsChanger;

        [SetUp]
        public void SetUp()
        {
            wordsProvider = Substitute.For<IWordsProvider>();
            filter = Substitute.For<IFilter>();
            wordsChanger = Substitute.For<IWordsChanger>();
            words = new []{"A", "aa", "b"};
            wordsProvider.Provide().ReturnsForAnyArgs(words);
            filter.FilterWords(Arg.Any<IEnumerable<string>>()).ReturnsForAnyArgs(callInfo => callInfo.Arg<IEnumerable<string>>());
            wordsChanger.ChangeWords(Arg.Any<IEnumerable<string>>())
                .ReturnsForAnyArgs(callInfo => callInfo.Arg<IEnumerable<string>>().Select(w => w.ToUpper()));
            preprocessor = new TextPreprocessor(wordsProvider, new []{filter}, new []{wordsChanger});
        }

        [Test]
        public void ProvidePreprocessedWords_WhenFiltering()
        {
            var expected = new[] { "aa" };
            filter.FilterWords(words).Returns(expected);
            wordsChanger.ChangeWords(expected).Returns(callInfo => callInfo.Arg<IEnumerable<string>>());
            preprocessor.Provide().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ProvidePreprocessedWords_WhenChanging()
        {
            var expected = new[] { "A", "AA", "B"};
            preprocessor.Provide().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ProvidePreprocessedWords_WhenFilteringAndChanging()
        {
            var expected = new[] { "A", "B"};
            filter.FilterWords(Arg.Any<IEnumerable<string>>()).Returns(expected);
            preprocessor.Provide().Should().BeEquivalentTo(expected);
        }
    }
}
