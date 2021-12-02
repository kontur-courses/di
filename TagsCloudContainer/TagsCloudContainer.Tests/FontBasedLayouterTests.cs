using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layout;
using TagsCloudVisualization;

namespace TagsCloudContainer.Tests
{
    public class FontBasedLayouterTests
    {
        private FontBasedLayouter layouter;
        private List<string> words;

        [SetUp]
        public void SetUp()
        {
            layouter = new FontBasedLayouter(FontFamily.GenericMonospace,
                new FrequencyLinearFontSizeSelector(new FontSizeRange(32, 12)), new CircularCloudLayouter());

            words = new List<string>(Enumerable.Repeat("a", 10).Concat(Enumerable.Repeat("b", 5)));
        }

        [Test]
        public void GetCloudLayout_AllWordLocationsNotNegative()
        {
            var locations = layouter.GetCloudLayout(words).WordLayouts
                .Select(wordLayout => wordLayout.Location);

            foreach (var location in locations)
            {
                location.X.Should().BeGreaterOrEqualTo(0);
                location.Y.Should().BeGreaterOrEqualTo(0);
            }
        }

        [Test]
        public void GetCloudLayout_PictureSizeIsPositive()
        {
            var size = layouter.GetCloudLayout(words).ImageSize;
            size.Width.Should().BePositive();
            size.Height.Should().BePositive();
        }

        [Test]
        public void GetCloudLayout_LeftOnlyUniqueWords()
        {
            layouter.GetCloudLayout(words)
                .WordLayouts.Select(wordLayout => wordLayout.Word)
                .Should().BeEquivalentTo("a", "b");
        }
    }
}