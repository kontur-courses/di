using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudContainerTests.TextAnalyzingTests
{
    internal class TagCreatorTests
    {
        private FilesSettings filesSettings;
        private ImageSettings imageSettings;

        [SetUp]
        public void SetUp()
        {
            imageSettings = new ImageSettings();
            filesSettings = new FilesSettings();
            filesSettings.TextFileName = "testTextAnalyzer.txt";
        }

        [Test]
        public void GetTagsForVisualization_ReturnTagsWithCorrectFontSize()
        {
            imageSettings.MinFontSize = 1;
            var tagCreator = new TagCreator(imageSettings, new SpiralCloudLayouter(imageSettings),
                new TextAnalyzer(filesSettings));
            var actual = tagCreator.GetTagsForVisualization().ToArray();
            var expectedFontSizes = new[] {33, 1};
            var expectedWords = new[] {"a", "c"};
            actual.Length.Should().Be(expectedWords.Length);
            for (var i = 0; i < expectedWords.Length; i++)
            {
                actual[i].FontSize.Should().Be(expectedFontSizes[i]);
                actual[i].Text.Should().Be(expectedWords[i]);
            }
        }

        [Test]
        public void GetTagsForVisualization_NotReturnTagsWithFontSizeSmallerMinFontSize()
        {
            var tagCreator = new TagCreator(imageSettings, new SpiralCloudLayouter(imageSettings),
                new TextAnalyzer(filesSettings));
            var actual = tagCreator.GetTagsForVisualization().ToArray();
            var expectedFontSizes = new[] {33};
            var expectedWords = new[] {"a"};
            actual.Length.Should().Be(expectedWords.Length);
            for (var i = 0; i < expectedWords.Length; i++)
            {
                actual[i].FontSize.Should().Be(expectedFontSizes[i]);
                actual[i].Text.Should().Be(expectedWords[i]);
            }
        }
    }
}