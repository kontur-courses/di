using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Frequency;
using TagsCloudVisualization.Storages;

namespace TagsCloudVisualization.TagsCloudVisualizationTests
{
    [TestFixture]
    internal class FrequencyCounterTests
    {
        [Test]
        public void GetFrequency_ShouldCorrectlyCountTheFrequency_WhenThereIsOneWordInText()
        {
            var text = new[] {"Я", "Я", "Я", "Я", "Я", "Я", "Я", "Я" };
            var frCounter = new FrequencyCounter(new WordStorage(text));
            frCounter.GetFrequency().Should().BeEquivalentTo(new Dictionary<string, int>() { { "Я", 8 } });
        }

        [Test]
        public void GetFrequency_ShouldCorrectlyCountTheFrequency_WhenThereIsManyWordInText()
        {
            var text = new[] { "1", "2", "1", "3", "1", "2", "4", "4", "4", "5", "5", "1" };
            var expected = new Dictionary<string, int>()
            {
                { "1", 4 },
                { "2", 2 },
                { "3", 1 },
                { "4", 3 },
                { "5", 2 },
            };
            var frCounter = new FrequencyCounter(new WordStorage(text));
            frCounter.GetFrequency().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetFrequency_ShouldBeEmpty_WhenTextIsEmpty()
        {
            var text = new string[] {};
            var frCounter = new FrequencyCounter(new WordStorage(text));
            frCounter.GetFrequency().Should().BeEmpty();
        }
    }
}
