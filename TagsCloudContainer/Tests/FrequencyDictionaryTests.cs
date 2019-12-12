using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Core;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    class FrequencyDictionaryTests
    {
        private Dictionary<string, int> frequencyDictionary;

        [SetUp]
        public void SetUp()
        {
            frequencyDictionary = new Dictionary<string, int>();;
        }

        [Test]
        public void Constructor_DoesNotThrow_WhenWithoutParameters()
        {
            Action action = () => new Dictionary<string, int>();
            action.Should().NotThrow();
        }

        [TestCase(1, "bla", "bla", TestName = "WhenAddedTheseWordsOnce")]
        [TestCase(5, "a", "a", "a", "a", "a", "a", TestName = "WhenAddedTheseWordsFiveTimes")]
        [TestCase(1, "a", "a", "b", TestName = "WhenNotOnlyThisWordWasAdded")]
        public void Add_ShouldIncrementCounter(int expectedCounter, string verifiedKey, params string[] words)
        {
            foreach (var word in words)
                frequencyDictionary.Add(word);

            frequencyDictionary.GetCounter(verifiedKey).Should().Be(expectedCounter);
        }

        [Test]
        public void Top_ShouldReturnsMostFrequent()
        {
            for (var i = 0; i < 5; i++)
                frequencyDictionary.Add("a");
            for (var i = 0; i < 3; i++)
                frequencyDictionary.Add("b");
            for (var i = 0; i < 10; i++)
                frequencyDictionary.Add("c");

            frequencyDictionary.Top(3).Should().BeInDescendingOrder(kvp => kvp.Item2);
        }

    }
}
