using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Painters;
using TagCloud.Core.Settings.DefaultImplementations;
using TagCloud.Core.Util;

namespace TagCloud.Tests.Painters
{
    [TestFixture]
    public class OneColorPainterTests
    {
        private PaintingSettings settings;
        private OneColorPainter painter;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            settings = new PaintingSettings();
            painter = new OneColorPainter(settings);
        }

        [Test]
        public void PaintTags_ShouldThrowArgumentNullException_WhenNullIsGiven()
        {
            Action tagsPaintingAction = () => painter.PaintTags(null);
            tagsPaintingAction.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void PaintTags_ShouldPaintAllGivenTags()
        {
            var tags = new[] {"a", "b", "c"}
                .Select(w => new TagStat(w, 1))
                .Select(tagStat => new Tag(tagStat, null, new RectangleF()))
                .ToList();

            painter.PaintTags(tags);

            tags.All(tag => tag.Brush.Equals(settings.TagBrush)).Should().BeTrue();
        }
    }
}