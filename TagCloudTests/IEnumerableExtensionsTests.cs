using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;

namespace TagCloudTests
{
    [TestFixture]
    class IEnumerableExtensionsTests
    {
        [TestCase("a", TestName = "OneElement")]
        [TestCase("a", "a", "a", TestName = "OneDifferentItem")]
        [TestCase("a", "a", "b", TestName = "ElementsHaveDifferentFrequencies")]
        [TestCase("a", "b", "c", TestName = "AllElementsHaveSameFrequency")]
        public void MostFrequent_ShouldReturnElementsInDescendingOrder(params string[] words)
        {
            words.MostFrequent(words.Length).Should().BeInDescendingOrder(el => el.Item2);
        }

        [TestCase(new[] { "a" }, new[] { 1 }, TestName = "OneElement")]
        [TestCase(new[] { "a", "a", "a" }, new[] { 3 }, TestName = "OneDifferentItem")]
        [TestCase(new[] { "a", "a", "b" }, new[] { 2, 1 }, TestName = "ElementsHaveDifferentFrequencies")]
        [TestCase(new[] { "a", "b", "c" }, new[] { 1, 1, 1 }, TestName = "AllElementsHaveSameFrequency")]
        public void MostFrequent_ShouldReturnElementsWithTheirFrequencies(
            string[] words, int[] expectedFrequencies)
        {
            words.MostFrequent(words.Length)
                .Select(el => el.Item2).Should().BeEquivalentTo(expectedFrequencies);
        }

        [TestCase(new[] { "a", "b" }, 2, TestName = "AmountToTakeIsBiggerThanDifferentItemsAmount")]
        [TestCase(new[] { "a", "b" }, 2, TestName = "AmountToTakeAsSameAsDifferentItemsAmount")]
        public void MostFrequent_ShouldReturnAllDifferentElements(
            string[] words, int differentItemsCount)
        {
            words.MostFrequent(differentItemsCount + 1).Count()
                .Should().Be(differentItemsCount);
        }

        [TestCase("a", "b", "c", "a", "b", "c", "a", "b", "c")]
        public void MostFrequent_ShouldReturnOnlyDifferentElements(params string[] words)
        {
            var mostFrequent = words.MostFrequent(words.Length).ToList();

            mostFrequent
                .Select((item, index) => mostFrequent.Skip(index + 1).Count(item.Equals))
                .Sum().Should().Be(0);
        }
    }
}