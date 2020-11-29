using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudContainerTests.TextAnalyzingTests
{
    internal class TextAnalyzerTests
    {
        private string boringWords;
        private FilesSettings filesSettings;

        [SetUp]
        public void SetUp()
        {
            filesSettings = new FilesSettings();
            boringWords = "b, d";
        }

        [Test]
        public void GetWordsWithFrequency_FromLine()
        {
            var text = "a, b; c\na a b. d";
            var actual = new TextAnalyzer(text, boringWords).GetWordWithFrequency();
            var expected = new Dictionary<string, int> {{"a", 3}, {"c", 1}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromEmptyLine()
        {
            var text = "";
            var actual = new TextAnalyzer(text, boringWords).GetWordWithFrequency();
            var expected = new Dictionary<string, int>();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromTxtFile()
        {
            filesSettings.TextFileName = "testTextAnalyzer.txt";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int> {{"a", 3}, {"c", 1}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromEmptyTxtFile()
        {
            filesSettings.TextFileName = "testEmptyTextAnalyzer.txt";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int>();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}