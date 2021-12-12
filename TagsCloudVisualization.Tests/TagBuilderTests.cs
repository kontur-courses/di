using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.Tags;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagBuilderTests
    {
        private IFileReader fakeReader;
        private ITextAnalyzer fakeTextAnalyzer;
        private TagBuilder tagBuilder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            fakeReader = A.Fake<IFileReader>();
            fakeTextAnalyzer = A.Fake<ITextAnalyzer>();
            tagBuilder = new TagBuilder(fakeReader, fakeTextAnalyzer);
        }

        [TestCase("облако", 1, 1)]
        [TestCase("Ленин", 3, 1)]
        [TestCase("nag1bat0r", 1, 10)]
        [DefaultFloatingPointTolerance(0.0001)]
        public void GetTags_ShouldCorrectCountTagWeight_ForSingleWord(string input, int inputCount, int numOfLines)
        {
            var fakeWordStatistic = new Dictionary<string, int> {{input, inputCount}};
            A.CallTo(() => fakeReader.ReadLines(string.Empty)).Returns(Enumerable.Repeat(string.Empty, numOfLines));
            A.CallTo(() => fakeTextAnalyzer.GetWordStatistics(string.Empty)).Returns(fakeWordStatistic);

            var actual = tagBuilder.GetTags(string.Empty).ToList();
            actual.Should().ContainSingle(tag => tag.Weight == 1);
        }

        [Test]
        [DefaultFloatingPointTolerance(0.0001)]
        public void GetTags_ShouldCorrectCountTagWeight_ForMultipleWords()
        {
            const int numOfLines = 10;
            var fakeWordStatistic = new Dictionary<string, int>
            {
                {"облако", 1},
                {"Ленин", 3},
                {"nag1bat0r", 1}
            };

            A.CallTo(() => fakeReader.ReadLines(string.Empty)).Returns(Enumerable.Repeat(string.Empty, numOfLines));
            A.CallTo(() => fakeTextAnalyzer.GetWordStatistics(string.Empty)).Returns(fakeWordStatistic);

            var actual = tagBuilder.GetTags(string.Empty).ToList();
            actual.Should().Contain(tag => tag.Text == "облако" && tag.Weight == (float) 1 / 5);
            actual.Should().Contain(tag => tag.Text == "Ленин" && tag.Weight == (float) 3 / 5);
            actual.Should().Contain(tag => tag.Text == "nag1bat0r" && tag.Weight == (float) 1 / 5);
        }
    }
}