using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainerTests
{
    public class TagCloudDrawerTests
    {
        private IAppSettings settings;
        private IDrawer tagCloudDrawer;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            settings = new AppSettings()
            {
                ImageWidth = 1000,
                ImageHeight = 1000,
                BackgroundColorName = "White",
                FontColorName = "Black"
            };

            tagCloudDrawer = new TagCloudDrawer(settings);
        }
        
        [Test]
        public void DrawImage_ThrowsArgumentException_WhenTagCollectionIsEmpty()
        {
            Action act = () => tagCloudDrawer.DrawImage(new List<Tag>());

            act.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void DrawImage_CreatesImageWithSizeFromSettings()
        {
            var randomTag = new Tag("asd", new Rectangle(Point.Empty, new Size(10, 10)), new Font("Arial", 10));
            
            var image = tagCloudDrawer.DrawImage(new List<Tag>() { randomTag });

            image.Size.Height.Should().Be(settings.ImageHeight);
            image.Size.Width.Should().Be(settings.ImageWidth);
        }
    }
}