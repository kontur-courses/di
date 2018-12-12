using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Models;
using TagCloud.Visualizer;
using TagCloud.Visualizer.Settings;
using TagCloud.Visualizer.Settings.Colorizer;

namespace TagCloud.Tests
{
    [TestFixture]
    public class CloudVisualizer_Should
    {
        private readonly CloudVisualizer visualizer =
            new CloudVisualizer(
                new DrawSettings(
                    DrawFormat.OnlyRectangles,
                    new Font("Arial", 15),
                    Color.White,
                    new SolidColorizer(Color.Black)));

        [TestCase(1, TestName = "For 1 rectangle")]
        [TestCase(5, TestName = "For 5 rectangles")]
        public void CreatePictureWithRectangles(int amountOfRectangles)
        {
            var items = new CloudItem[amountOfRectangles];
            for (var i = 0; i < amountOfRectangles; i++)
                items[i] = new CloudItem(null, new Rectangle(0, 0, 10, 10));

            var picture = visualizer.CreatePictureWithItems(items);

            IsPictureContainsAllLocationPoints(items, picture)
                .Should().BeTrue();
        }

        public bool IsPictureContainsAllLocationPoints(CloudItem[] items, Bitmap picture)
        {
            return items.All(item =>
                picture.GetPixel(item.Bounds.Location.X, item.Bounds.Location.Y).ToArgb() != Color.White.ToArgb());
        }

        [TestCase(false, TestName = "Then items list is null")]
        [TestCase(true, TestName = "Then items list is empty")]
        public void ThrowArgumentException(bool isArrayInitialized)
        {
            Action creation = ()
                => visualizer.CreatePictureWithItems(
                    isArrayInitialized
                        ? new CloudItem[0]
                        : null);

            creation.Should().Throw<ArgumentException>();
        }
    }
}