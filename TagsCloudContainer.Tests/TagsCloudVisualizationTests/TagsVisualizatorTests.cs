using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class TagsVisualizatorTests
    {
        private TagsVisualizator TagsVisualizator { get; set; }

        [SetUp]
        public void SetUp()
        {
            TagsVisualizator = new TagsVisualizator();
        }

        [Test]
        public void GetBitmap_GetImageSizeThrowException_WhenRectangleOutOfBoundaries()
        {
            var rectangles = new List<Rectangle>();
            var location = new Point(-100, -100);
            var size = new Size(50, 50);
            rectangles.Add(new Rectangle(location, size));

            Action saveImage = () => TagsVisualizator.GetBitmap(rectangles);

            saveImage.Should().Throw<ArgumentException>();
        }
    }
}