using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudApp;

namespace TagsCloud.Tests
{
    public class DictionaryObjectParser
    {
        private DictionaryObjectParser<int, double> parser;
        private Mock<IObjectParser<int>> keyParserMock;
        private Mock<IObjectParser<double>> valueParserMock;

        private const int key1 = 11;
        private const double value1 = 1.1;
        private KeyValuePair<int, double> pair1;
        private const int key2 = 22;
        private const double value2 = 2.2;
        private KeyValuePair<int, double> pair2;

        [SetUp]
        public void SetUp()
        {
            pair1 = new KeyValuePair<int, double>(key1, value1);
            pair2 = new KeyValuePair<int, double>(key2, value2);
            keyParserMock = new Mock<IObjectParser<int>>();
            valueParserMock = new Mock<IObjectParser<double>>();
            MockParsers(keyParserMock, valueParserMock, pair1, pair2);
            parser = new DictionaryObjectParser<int, double>(keyParserMock.Object, valueParserMock.Object);
        }

        [Test]
        public void Parse_OnePair()
        {
            var input = $"({key1} {value1})";
            var actual = parser.Parse(input);
            AssertDictionary(actual, pair1);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(",")]
        [TestCase("aaa")]
        public void Parse_TwoPairs_WithSeparator(string separator)
        {
            var input = $"({key1} {value1}){separator}({key2} {value2})";
            var actual = parser.Parse(input);
            AssertDictionary(actual, pair1, pair2);
        }

        [Test]
        public void Parse_WithSameKeys_ContainLastKeyOccurrence()
        {
            var input = $"({key1} {value2})({key1} {value1})";
            var actual = parser.Parse(input);
            AssertDictionary(actual, pair1);
        }

        [TestCaseSource(nameof(ParseIgnoreIncompletePairs))]
        public void Parse_IgnoreIncompletePairs(string incompletePair)
        {
            var input = $"{incompletePair}({key2} {value2})";
            var actual = parser.Parse(input);
            AssertDictionary(actual, pair2);
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

        private static void AssertDictionary(IDictionary<int, double> actual, params KeyValuePair<int, double>[] pairs)
        {
            var expected = new Dictionary<int, double>(pairs);
            actual.Should().BeEquivalentTo(expected);
        }

        private static void MockParsers(Mock<IObjectParser<int>> keyParserMock,
            Mock<IObjectParser<double>> valueParserMock, params KeyValuePair<int, double>[] pairs)
        {
            foreach (var (key, value) in pairs)
            {
                keyParserMock.Setup(p => p.Parse(key.ToString())).Returns(key);
                valueParserMock.Setup(p => p.Parse(value.ToString())).Returns(value);
            }
        }
    }
}