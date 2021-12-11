using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using TagsCloudContainer;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class FrequencyPainterTests
    {
        private Tag[] tags;
        private ITagPainter painter;

        [OneTimeSetUp]
        public void SetUp()
        {
            tags = new[]
            {
                new Tag(0.3, "First", WordType.Default),
                new Tag(0.5, "Second", WordType.Default),
                new Tag(0.2, "Third", WordType.Default)
            };
            var settings = SettingsProvider.GetSettings();
            painter = new FrequencyTagPainter(settings);
        }

        [Test]
        public void Should_PaintCorrectly()
        {
            var result = painter.Paint(tags);

            result.OrderByDescending(tag => tag.Color.A).First()
                .Text.Should().BeEquivalentTo("Second");
        }
    }
}
