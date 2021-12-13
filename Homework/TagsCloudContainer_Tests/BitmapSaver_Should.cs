using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.BitmapSaver;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class BitmapSaver_Should
    {
        [SetUp]
        public void SetUp()
        {
            testingBmp = new Bitmap(1920, 1080);
        }

        private readonly string currentDirectory = Environment.CurrentDirectory;
        private readonly BitmapSaver sut = new();

        private Bitmap testingBmp;


        [Test]
        public void Save_WhenCorrectPath()
        {
            var testPath = Path.Combine(currentDirectory, "test.png");
            sut.Save(testingBmp, testPath);
            File.Exists(testPath).Should().BeTrue();
            File.Delete(testPath);
        }

        [Test]
        public void CreateDirectory_WhenDirectoryNotExist()
        {
            var testPath = Path.Combine(currentDirectory, "notexistingdirectory");
            if (Directory.Exists(testPath))
                Directory.Delete(testPath, true);
            Directory.Exists(testPath).Should().BeFalse();
            sut.Save(testingBmp, Path.Combine(testPath, "test.png"));
            Directory.Exists(testPath).Should().BeTrue();
            Directory.Delete(testPath, true);
        }

        [Test]
        public void ThrowWithMessage_WhenDisposedBitmap()
        {
            testingBmp.Dispose();
            Action act = () => sut.Save(testingBmp, "test.png");
            act.Should().Throw<Exception>().WithMessage("Не удалось сохранить файл");
        }
    }
}