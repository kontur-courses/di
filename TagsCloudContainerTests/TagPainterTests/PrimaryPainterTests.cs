﻿using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using TagsCloudContainer;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class PrimaryPainterTests
    {
        private Tag[] tags;
        private ITagPainter painter;
        private Palette palette;

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
            palette = settings.Palette;
            painter = new PrimaryTagPainter(settings);
        }

        [Test]
        public void Should_PaintCorrectly()
        {
            var result = painter.Paint(tags);

            result.Select(tag => tag.Color)
                .Should().AllBeEquivalentTo(palette.Primary);
        }
    }
}
