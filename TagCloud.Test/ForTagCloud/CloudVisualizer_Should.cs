using System;
using System.Drawing;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Models;
using TagCloud.Visualizer;

namespace TagCloud.Tests.ForTagCloud
{
    [TestFixture]
    public class CloudVisualizer_Should : TestBase
    {
        private CloudVisualizer sut;

        [SetUp]
        public void SetUp()
        {
            sut = container.Resolve<CloudVisualizer>();
        }

        [TestCase(1, TestName = "For 1 rectangle")]
        [TestCase(5, TestName = "For 5 rectangles")]
        public void CreatePictureWithRectangles(int amountOfRectangles)
        {
            var items = new TagItem[amountOfRectangles];
            for (var i = 0; i < amountOfRectangles; i++)
                items[i] = new TagItem("1", 1);

            var picture = sut.CreatePictureWithItems(items);

            IsPictureContainsAllLocationPoints(items, picture)
                .Should().BeTrue();
        }

        public bool IsPictureContainsAllLocationPoints(TagItem[] items, Bitmap picture)
        {
            var count = 0;
            for (var x = 0; x < picture.Width; x++)
            {
                for (var y = 0; y < picture.Height; y++)
                {
                    if (picture.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        count++;
                    }
                }
            }

            return count >= items.Length;
        }

        [TestCase(false, TestName = "Then items list is null")]
        [TestCase(true, TestName = "Then items list is empty")]
        public void ThrowArgumentException(bool isArrayInitialized)
        {
            Action creation = ()
                => sut.CreatePictureWithItems(
                    isArrayInitialized
                        ? new TagItem[0]
                        : null);

            creation.Should().Throw<ArgumentException>();
        }
    }
}