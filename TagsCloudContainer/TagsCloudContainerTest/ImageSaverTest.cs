using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TagsCloudContainer;
using System.Drawing;
using System.IO;

namespace TagsCloudContainerTest
{
    public class ImageSaverTest
    {
        [Test]
        public void CheckSavingImageByPath()
        {
            var dirPath = @"..\..\..\Files";
            var imageName = "test";
            var saver = new ImageSaver(dirPath);

            saver.Save(new Bitmap(1, 1), imageName);

            File.Exists($"{dirPath}\\{imageName}.png").Should().BeTrue();
        }

        [Test]
        public void CheckSavingImageWithNotStandardExtension()
        {
            var dirPath = @"..\..\..\Files";
            var imageName = "test";
            var saver = new ImageSaver(dirPath, "tiff");

            saver.Save(new Bitmap(1, 1), imageName);

            File.Exists($"{dirPath}\\{imageName}.tiff").Should().BeTrue();
        }
    }
}
