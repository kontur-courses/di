using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Settings;
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
            var random = new Random();
            words = new List<string>(Enumerable.Repeat("a", 10).Concat(Enumerable.Repeat("b", 5)));

            var fontFamilySettings = Mock.Of<IFontFamilySettings>(
                s => s.FontFamily == FontFamily.GenericMonospace);

            var fontSizeSelector = Mock.Of<IFontSizeSelector>(
                s => s.GetFontSizedWords(words) == words.Select(w => new FontSizedWord(w, random.Next(10, 50))));

            layouter = new FontBasedLayouter(
                fontFamilySettings,
                fontSizeSelector,
                new CircularCloudLayouter());
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
    }
}