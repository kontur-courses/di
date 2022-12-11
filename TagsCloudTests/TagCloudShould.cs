using NUnit.Framework;
using System;
using TagsCloud;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudTests
{
    [TestFixture]
    public class TagCloudShould
    {
        private TagCloud tagCloud;

        private string textPath = @"..\..\..\..\text.txt";
        private string picPath = @"..\..\..\..\testPicture";
        private string extension = @".png";

        private ServiceProvider serviceProvider;

        [SetUp]
        public void SetUp()
        {
            serviceProvider = ContainerBuilder.GetNewTagCloudServices(1024, 720);
            tagCloud = serviceProvider.GetService<TagCloud>();
        }

        [Test]
        public void TagCloud_CommonInput_ShouldCreateFile()
        {
            var fullPath = picPath + extension;
            
            if (File.Exists(fullPath)) File.Delete(fullPath);

            tagCloud.PrintTagCloud(textPath, picPath, extension);

            File.Exists(fullPath).Should().BeTrue();
        }

        [Test]
        public void TagCloud_NullTextFile_ShouldThrowException()
        {
            Assert.Throws<FileNotFoundException>(() => tagCloud.PrintTagCloud(String.Empty, picPath, picPath));
        }

        [Test]
        public void TagCloud_NotCreatedTextFile_ShouldThroeExeption()
        {
            var path = @"..\..\..\..\testText.txt";

            if (File.Exists(path)) File.Delete(path);

            Assert.Throws<FileNotFoundException>(() => tagCloud.PrintTagCloud(path, picPath, extension));
        }
    }
}
