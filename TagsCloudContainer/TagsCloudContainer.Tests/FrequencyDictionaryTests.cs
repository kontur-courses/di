using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;

namespace TagsCloudContainer.Tests
{
    public class FrequencyDictionaryTests
    {
        private FrequencyDictionary<string> emptyDictionary;
        private FrequencyDictionary<string> defaultDictionary;

        [SetUp]
        public void SetUp()
        {
            emptyDictionary = new FrequencyDictionary<string>();
            defaultDictionary = new FrequencyDictionary<string>
            {
                "a",
                "a",
                "b"
            };
        }

        [Test]
        public void Add_AfterFirstItem_IsOne()
        {
            emptyDictionary.Add("qwe");

            emptyDictionary["qwe"]
                .Should().Be(1);
        }

        [Test]
        public void Add_Increase_Value()
        {
            emptyDictionary.Add("qwe");
            emptyDictionary.Add("qwe");

            emptyDictionary["qwe"]
                .Should().Be(2);
        }

        [Test]
        public void Add_Increase_DifferentValues()
        {
            defaultDictionary["a"]
                .Should().Be(2);
        }

        [Test]
        public void Keys_ReturnAllKeys()
        {
            emptyDictionary.Add("a");
            emptyDictionary.Add("b");

            emptyDictionary.Keys
                .Should().BeEquivalentTo("a", "b");
        }

        [Test]
        public void Values_ReturnsAllValues()
        {
            defaultDictionary.Values
                .Should().BeEquivalentTo(new[] {1, 2});
        }

        [Test]
        public void GetEnumerator_ReturnsAllKeyValues()
        {
            defaultDictionary.ToList()
                .Should().BeEquivalentTo(new List<KeyValuePair<string, int>>
                {
                    new("a", 2),
                    new("b", 1),
                });
        }

        [Test]
        public void AddRange_AddAllElements()
        {
            emptyDictionary.AddRange(new List<string> {"a", "a", "b"});
            emptyDictionary.Should().BeEquivalentTo(new Dictionary<string, int>
            {
                ["a"] = 2,
                ["b"] = 1,
            });
        }

        [Test]
        public void Constructor_AddRange()
        {
            var dictionary = new FrequencyDictionary<string>(new[] {"a", "a", "b"});
            dictionary.Should().BeEquivalentTo(new Dictionary<string, int>
            {
                ["a"] = 2,
                ["b"] = 1,
            });
        }
    }
}