using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Common.ImageWriters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ImageWriterTests
    {
        private ImageWriter writer;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            writer = new ImageWriter();
        }

        [TestCase("test.bmp")]
        [TestCase("test.gif")]
        [TestCase("test.ico")]
        [TestCase("test.jpg")]
        [TestCase("test.jpeg")]
        [TestCase("test.png")]
        [TestCase("test.tif")]
        [TestCase("test.tiff")]
        [TestCase("test.wmf")]
        public void Save_ShouldWorksCorrectly_WithSupportedImageFormats(string filename)
        {
            var action = new Action(() =>
            {
                using var bitmap = new Bitmap(1, 1);
                writer.Save(bitmap, filename);
            });

            action.Should().NotThrow();
        }
        
        [TestCase("test.mp3")]
        [TestCase("test.doc")]
        public void Save_ShouldThrowArgumentException_WhenPassUnknownImageFormat(string filename)
        {
            var action = new Action(() =>
            {
                using var bitmap = new Bitmap(1, 1);
                writer.Save(bitmap, filename);
            });

            action.Should().Throw<ArgumentException>();
        }
    }
}