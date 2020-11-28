using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests
{
    public class TagGeneratorTests
    {
        private TagGenerator TagGenerator { get; set; }

        [SetUp]
        public void SetUp()
        {
            var center = new Point(200, 200);
            TagGenerator = new TagGenerator(new CircularCloudLayouter(center));
        }

        [TestCaseSource(nameof(TestCases))]
        public void ShouldReturnExpectedCount_When(Dictionary<string, int> wordEntry, int expectedCount)
        {
            var result = TagGenerator.GetTags(wordEntry);

            result.Should().HaveCount(expectedCount);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(new Dictionary<string, int> {{"one", 1}}, 1).SetName("One word");
        }
    }
}