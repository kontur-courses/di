using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud;
using System.IO;
using FluentAssertions;

namespace TagsCloudTests
{
    [TestFixture]
    public class TagCloudShould
    {
        private PrintSettings settings;

        [SetUp]
        public void CreateSettings()
        {
            settings = new PrintSettings();
            settings.SetFont("Consolas", 64);
            settings.SetCentralPen(Color.Black, 8);
            settings.SetSurroundPen(Color.Black, 4);
            settings.SetBackgroudColor(Color.White);
        }


        [Test]
        public void TagCloud_CommonInput_ShouldCreateFile()
        {
            string appPath = @"C:\Users\User\source\repos\DI\di\";
            var textName = "text.txt";
            var captureName = @"testPicture";
            var extension = @".png";

            var tagCloud = new TagCloud(settings, appPath + textName);

            var fullPath = appPath + captureName + extension;
            
            if (File.Exists(fullPath)) File.Delete(fullPath);

            tagCloud.PrintTagCloud(appPath + captureName, extension);

            File.Exists(fullPath).Should().BeTrue();
        }

        [Test]
        public void TagCloud_NullTextFile_ShouldThrowExeption()
        {
            Assert.Throws<FileNotFoundException>(() => new TagCloud(settings, String.Empty));
        }

        [Test]
        public void TagCloud_NotCreatedTextFile_ShouldThroeExeption()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "testText.txt";

            if (File.Exists(path)) File.Delete(path);

            Assert.Throws<FileNotFoundException>(() => new TagCloud(settings, path));
        }
    }
}
