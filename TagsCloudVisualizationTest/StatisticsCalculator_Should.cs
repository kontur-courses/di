using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Utils;


namespace TagsCloudVisualizationTest
{
    [TestFixture]
    class StatisticsCalculator_Should
    {
        private readonly StatisticsCalculator statisticsCalculator = new StatisticsCalculator();

        [Test]
        public void ReturnEmptyStatistics_WhenNoWords()
        {
            var statistics = statisticsCalculator.CalculateStatistics(new string[] { });

            statistics.OrderedWordsStatistics.Should().BeEmpty();
            statistics.AllWordsCount.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void ReturnRightAllWordsCount_WhenAllWordsUnique()
        {
            var statistics = statisticsCalculator.CalculateStatistics(new [] { "car", "phone" });
            statistics.AllWordsCount.Should().Be(2);
        }

        [Test]
        public void ReturnRightAllWordsCount_WhenSomeWordsAreTheSame()
        {
            var statistics = statisticsCalculator.CalculateStatistics(new[] { "car", "phone", "car" });
            statistics.AllWordsCount.Should().Be(3);
        }

        [Test]
        public void CountWordsCorrectly()
        {
            var statistics = statisticsCalculator.CalculateStatistics(new[] { "car", "phone", "car" });
            statistics.OrderedWordsStatistics.Should().BeEquivalentTo(
                new WordStatistics("car", 2), new WordStatistics("phone", 1));

        }

        [Test]
        public void OrderWordStatistics()
        {
            var statistics = statisticsCalculator.CalculateStatistics(new[] { "car", "phone", "car" });
            statistics.OrderedWordsStatistics.Select(ws => ws.Count).Should().BeInDescendingOrder();
        }
    }}
