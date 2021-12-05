using FluentAssertions;
using NUnit.Framework;
using System;
using System.Drawing;
using System.IO;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class CloudImageGeneratorTests
    {
        private Point testPoint = new Point(1000, 1000);
        private OvalCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new OvalCloudLayouter(testPoint, ArchimedeanSpiral.Create(testPoint));
        }

        [Test]
        public void Should_Throw_WhenTryingToCreateWithNoRectangles()
        {
            FluentActions.Invoking(
                () => CloudImageGenerator.CreateImage(layouter.Cloud, new Size(testPoint)))
                .Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_SaveToFile()
        {
            layouter.PutNextRectangle(new Size(testPoint));
            var path = CloudImageGenerator.CreateImage(layouter.Cloud, new Size(testPoint));
            File.Exists(path).Should().BeTrue();
            File.Delete(path);
        }
    }
}
