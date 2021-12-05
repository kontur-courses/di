using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Visualization.WordsFilter;
using TagsCloud.Visualization.WordsParser;

namespace TagsCloud.Tests
{
    public class WordsParserTests
    {
        private WordsParser sut;

        [SetUp]
        public void InitParser()
        {
            sut = new WordsParser(new IWordsFilter[] {new BoringWordsFilter()});
        }

        [TestCaseSource(typeof(TestDataGenerator))]
        public void CountWordsFrequency_Should_ParseCorrectly_When(string text, Dictionary<string, int> expectedResult)
        {
            var result = sut.CountWordsFrequency(text);

            result.Should().Equal(expectedResult);
        }

        [Test]
        public void CountWordsFrequency_Should_ThrowException_OnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => sut.CountWordsFrequency(null));
        }
    }

    public class TestDataGenerator : IEnumerable<TestCaseData>
    {
        public IEnumerator<TestCaseData> GetEnumerator()
        {
            yield return new TestCaseData("", new Dictionary<string, int>()).SetName("Empty text");
            yield return new TestCaseData("    ", new Dictionary<string, int>()).SetName("Whitespace text");
            yield return new TestCaseData(", , , , ,", new Dictionary<string, int>()).SetName("Only commas");
            yield return new TestCaseData("test test test", new Dictionary<string, int> {{"test", 3}}).SetName(
                "Simple text");
            yield return new TestCaseData("test Test TEST", new Dictionary<string, int> {{"test", 3}}).SetName(
                "Different case");
            yield return new TestCaseData("test,test,test", new Dictionary<string, int> {{"test", 3}}).SetName(
                "Separated by comma");
            yield return new TestCaseData("test\ntest\ntest", new Dictionary<string, int> {{"test", 3}}).SetName(
                "Separated by new line");
            yield return new TestCaseData("hello world hello world",
                    new Dictionary<string, int> {{"hello", 2}, {"world", 2}})
                .SetName("Two different words");
            yield return new TestCaseData("тест test", new Dictionary<string, int> {{"test", 1}, {"тест", 1}})
                .SetName("Different languages");
            yield return new TestCaseData("1234 1234", new Dictionary<string, int> {{"1234", 2}}).SetName("Digits");
            yield return new TestCaseData("Another brick in the wall",
                    new Dictionary<string, int> {{"another", 1}, {"brick", 1}, {"wall", 1}})
                .SetName("Boring words");
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}