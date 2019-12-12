using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextProcessing.Shufflers;

namespace TagsCloudTextProcessing.Tests
{
    [TestFixture]
    public class ShufflersTests
    {
        [Test]
        public void DescendingShuffler_Shuffle_Should_SortTokensByTokenCountDescending()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new DescendingCountShuffler().Shuffle(tokens);

            shuffledTokens.Should().BeInDescendingOrder(t => t.Count);
        }

        [Test]
        public void DescendingShuffler_Shuffle_Should_NotChangeElementsOfCollection()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new DescendingCountShuffler().Shuffle(tokens);

            tokens.Should().BeEquivalentTo(shuffledTokens, o => o.WithoutStrictOrdering());
        }

        [Test]
        public void AscendingShuffler_Shuffle_Should_SortTokensByTokenCountAscending()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new AscendingCountShuffler().Shuffle(tokens);

            shuffledTokens.Should().BeInAscendingOrder(t => t.Count);
        }

        [Test]
        public void AscendingShuffler_Shuffle_Should_NotChangeElementsOfCollection()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new AscendingCountShuffler().Shuffle(tokens);

            tokens.Should().BeEquivalentTo(shuffledTokens, o => o.WithoutStrictOrdering());
        }

        [Test]
        public void RandomShuffler_Shuffle_Should_SortTokensRandomly()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };
            var expectedTokens = new[]
            {
                new Token("d", 2),
                new Token("b", 1),
                new Token("a", 5),
                new Token("f", 7)
            };

            var shuffledTokens = new RandomShuffler(42).Shuffle(tokens).ToArray();

            shuffledTokens.Should().BeEquivalentTo(expectedTokens, o => o.WithStrictOrdering());
        }

        [Test]
        public void RandomShuffler_Shuffle_Should_NotChangeElementsOfCollection()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new RandomShuffler(42).Shuffle(tokens);

            tokens.Should().BeEquivalentTo(shuffledTokens, o => o.WithoutStrictOrdering());
        }
    }
}