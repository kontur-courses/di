using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudTextPreparation.Shufflers;

namespace TagsCloudTextPreparation.Tests
{
    [TestFixture]
    public class ShufflersTests
    {
        [Test]
        public void TokenShufflerDescendingShuffle_Should_SortTokensByTokenCountDecending()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new TokenShufflerDescending().Shuffle(tokens);

            shuffledTokens.Should().BeInDescendingOrder(t => t.Count);
        }

        [Test]
        public void TokenShufflerDescendingShuffle_Should_SortTokensByTokenCountAscending()
        {
            var tokens = new[]
            {
                new Token("b", 1),
                new Token("a", 5),
                new Token("d", 2),
                new Token("f", 7)
            };

            var shuffledTokens = new TokenShufflerAscending().Shuffle(tokens);

            shuffledTokens.Should().BeInAscendingOrder(t => t.Count);
        }

        [Test]
        public void TokenShufflerRandomShuffle_Should_ShuffleTokensRandomly()
        {
            var tokensBeforeShuffling = new[]
            {
                new Token("b", 1),
                new Token("a", 2),
                new Token("d", 3),
                new Token("f", 4)
            };
            var expectedTokens = new[]
            {
                new Token("f", 4),
                new Token("d", 3),
                new Token("a", 2),
                new Token("b", 1)
            };

            var shuffledTokens = new TokenShufflerRandom(10).Shuffle(tokensBeforeShuffling);

            shuffledTokens.Should().BeEquivalentTo(expectedTokens, options => options.WithStrictOrdering());
        }
    }
}