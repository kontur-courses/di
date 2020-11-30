using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudVisualization.Tests
{
    public class TextParserTests
    {
        private TextParser Parser { get; set; }

        [SetUp]
        public void SetUp()
        {
            Parser = new TextParser(new WordValidator());
        }

        [TestCaseSource(nameof(TestCases))]
        public void ShouldReturnExpectedResult_When(string text, Dictionary<string, int> expectedResult)
        {
            Parser.GetParsedText(text).Should().BeEquivalentTo(expectedResult);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData("one two three",
                new Dictionary<string, int> {{"one", 1}, {"two", 1}, {"three", 1}}).SetName("Just words");
            yield return new TestCaseData($"new. {Environment.NewLine} line.",
                new Dictionary<string, int> {{"new", 1}, {"line", 1}}).SetName("New line");
            yield return new TestCaseData("TEXT", new Dictionary<string, int> {{"text", 1}}).SetName("Upper letters");
            yield return new TestCaseData("Text", new Dictionary<string, int> {{"text", 1}}).SetName("Capitalised");
            yield return new TestCaseData("i am a boring", new Dictionary<string, int> {{"boring", 1}}).SetName(
                "Boring words");
            yield return new TestCaseData("one one two", new Dictionary<string, int> {{"one", 2}, {"two", 1}}).SetName(
                "More than one entry");
            yield return new TestCaseData("one Two two", new Dictionary<string, int> {{"one", 1}, {"two", 2}}).SetName(
                "Different capitalization");
        }
    }
}