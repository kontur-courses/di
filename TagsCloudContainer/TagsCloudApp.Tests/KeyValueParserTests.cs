using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudApp.Parsers;

namespace TagsCloud.Tests
{
    public class KeyValueParserTests
    {
        private KeyValueParser parser;

        private const string key1 = "key1";
        private const string value1 = "value1";
        private KeyValuePair<string, string> pair1;
        private const string key2 = "key2";
        private const string value2 = "value2";
        private KeyValuePair<string, string> pair2;

        [SetUp]
        public void SetUp()
        {
            parser = new KeyValueParser();
            pair1 = new KeyValuePair<string, string>(key1, value1);
            pair2 = new KeyValuePair<string, string>(key2, value2);
        }

        [Test]
        public void Parse_OnePair()
        {
            var input = $"({key1} {value1})";
            var actual = parser.Parse(input);
            AssertKeyValues(actual, pair1);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(",")]
        [TestCase("aaa")]
        public void Parse_TwoPairs_WithSeparator(string separator)
        {
            var input = $"({key1} {value1}){separator}({key2} {value2})";
            var actual = parser.Parse(input);
            AssertKeyValues(actual, pair1, pair2);
        }

        [Test]
        public void Parse_WithSameKeys_ContainsAllPairs()
        {
            var input = $"({key1} {value2})({key1} {value1})";
            var actual = parser.Parse(input);
            AssertKeyValues(actual, new KeyValuePair<string, string>(key1, value2), pair1);
        }

        [TestCaseSource(nameof(ParseIgnoreIncompletePairs))]
        public void Parse_IgnoreIncompletePairs(string incompletePair)
        {
            var input = $"{incompletePair}({key2} {value2})";
            var actual = parser.Parse(input);
            AssertKeyValues(actual, pair2);
        }

        private static IEnumerable<TestCaseData> ParseIgnoreIncompletePairs()
        {
            yield return new TestCaseData($"({key1})");
            yield return new TestCaseData($"( {key1})");
            yield return new TestCaseData($"({key1} )");
            yield return new TestCaseData($"( {key1} )");
            yield return new TestCaseData("(   )");
            yield return new TestCaseData("()");
        }

        private static void AssertKeyValues(
            IEnumerable<KeyValuePair<string, string>> actual,
            params KeyValuePair<string, string>[] pairs)
        {
            actual.Should().BeEquivalentTo(pairs);
        }
    }
}