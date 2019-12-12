using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Core.Extensions;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class EnumerableExtensionsTests
    {
        [TestCase("a", TestName = "WhenLengthIsOne")]
        [TestCase("a", "a", "a", TestName = "WhenOnlyOneDifferentItem")]
        [TestCase("a", "a", "b", TestName = "WhenOneElementMoreThanAnother")]
        [TestCase("a", "b", "c", TestName = "WhenAllElementsHaveSameFrequency")]
        public void MostCommon_ShouldReturnsMostFrequent(params string[] words)
        {
            words.MostCommon(3)
                .Should()
                .BeInDescendingOrder(kvp => kvp.Item2);
        }
    }
}