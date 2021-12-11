using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.ImageSaver;

namespace TagsCloudContainerTests
{
    public class ImageSaverTests
    {
        private readonly IImageSaver imageSaver = new ImageSaver();

        [Test]
        public void Save_ThrowsArgumentException_WhenFormatIsNotSupported()
        {
            Action act = () => imageSaver.Save(new Bitmap(5, 5), "image.asd");

            act.Should().Throw<ArgumentException>();
        }

        [TestCase("image.png", TestName = "WhenPngImage")]
        [TestCase("image.bmp", TestName = "WhenBmpImage")]
        [TestCase("image.jpg", TestName = "WhenJpgImage")]
        [TestCase("image.gif", TestName = "WhenGifImage")]
        public void Save_WorksCorrectlyWithSupportedFormats(string path)
        {
            var saver = new ImageSaver();
            var image = new Bitmap(5, 5);

            saver.Save(image, path);

            File.Exists(path).Should().BeTrue();

            File.Delete(path);
        }
    }
}