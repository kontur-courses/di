using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.MorphologicalAnalysis;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class MorphologicalAnalysisTests
    {
        [SetUp]
        public void SetUp()
        {
            var filePath = Path.Combine(Utilities.ProjectPath, "TestWords.txt");
            analyzer = new MorphologicalAnalyzer(filePath);
        }

        private MorphologicalAnalyzer analyzer;

        [Test]
        public void GetWords_ShouldReturnsNumberOfWordsCorrectly()
        {
            analyzer.GetWords().Should().HaveCount(10);
        }

        [Test]
        public void GetWords_ShouldReturnsLowerCaseWordsText()
        {
            analyzer.GetWords().ToList()
                .ForEach(word => word.Text.Should().BeLowerCased());
        }

        [Test]
        public void GetWords_ShouldNotReturnsEmptyWordText()
        {
            analyzer.GetWords().ToList()
                .ForEach(word => word.Text.Should().NotBeEmpty());
        }
    }
}