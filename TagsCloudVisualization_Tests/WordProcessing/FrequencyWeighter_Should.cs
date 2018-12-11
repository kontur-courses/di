using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests.WordProcessing
{
    [TestFixture]
    public class FrequencyWeighter_Should
    {
        private IWordsProvider wordsProvider;
        private FrequencyWeighter weighter;

        [SetUp]
        public void SetUp()
        {
            wordsProvider = Substitute.For<IWordsProvider>();
            weighter = new FrequencyWeighter(wordsProvider);
            wordsProvider.Provide().Returns(new[] { "a", "b" });
        }

        [Test]
        public void WeightWordsCorrectly_WhenRepeatingWords()
        {
            wordsProvider.Provide().Returns(new[] { "a", "b", "a" });
            var expected = new[] {new WeightedWord("a", 2), new WeightedWord("b", 1)};
            weighter.WeightWords().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void WeightWordsCorrectly_WhenNoRepeatingWords()
        {
           var expected = new[] {new WeightedWord("a", 1), new WeightedWord("b", 1)};
            weighter.WeightWords().Should().BeEquivalentTo(expected);
        }


        [Test]
        public void WeightWordsCorrectly_ReturnOrderedByDescendingAccordingWeight()
        {
            weighter.WeightWords().Should().BeInDescendingOrder(c => c.Weight);
        }

    }
}
