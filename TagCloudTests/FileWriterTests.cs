using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Writers;

namespace TagCloudTests
{
    public class FileWriterTests
    {
        private IFileWriter writer;

        [SetUp]
        public void SetUp()
        {
            writer = new BitmapWriter();
        }

        [Test]
        public void Write_ShouldWriteWithFilename()
        {
            var bitmap = new Bitmap(300, 300);

            writer.Write(bitmap, "test.png", "png");

            var fileInfo = new FileInfo(Environment.CurrentDirectory + "\\test.png");
            fileInfo.Exists.Should().BeTrue();
            fileInfo.Delete();
        }
    }
}
