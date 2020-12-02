using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class BitmapSaverTests
    {
        private Bitmap ImageBitmap { get; set; }
        private BitmapSaver BitmapSaver { get; set; }

        [SetUp]
        public void SetUp()
        {
            const int height = 10;
            const int width = 10;
            ImageBitmap = new Bitmap(width, height);
            BitmapSaver = new BitmapSaver();
        }

        [TestCase(@"<html></html>", TestName = "Directory dont exist")]
        [TestCase(@"C:_Dir_image.png", TestName = "Not backslash separator")]
        [TestCase(@"C:\image png", TestName = "Doesnt have dot separator")]
        [TestCase(@"C:\image.", TestName = "Doesnt have filename extension")]
        public void SaveBitmapToDirectory_ThrowException_When(string path)
        {
            Action saveImage = () => BitmapSaver.SaveBitmapToDirectory(ImageBitmap, path);

            saveImage.Should().Throw<ArgumentException>();
        }

        [Test]
        public void SaveBitmapToDirectory_DoesntThrowException_WhenRightPath()
        {
            var path = $"C:{Path.DirectorySeparatorChar}image.png";
            Action saveImage = () => BitmapSaver.SaveBitmapToDirectory(ImageBitmap, path);

            saveImage.Should().NotThrow<ArgumentException>();
        }
    }
}