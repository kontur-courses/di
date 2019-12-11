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
    }
}