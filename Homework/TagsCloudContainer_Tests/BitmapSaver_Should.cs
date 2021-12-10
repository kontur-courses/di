using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.BitmapSaver;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class BitmapSaver_Should
    {
        private readonly DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        private readonly BitmapSaver sut = new BitmapSaver();

        private Bitmap testingBmp;

        [SetUp]
        public void SetUp()
        {
            testingBmp = new Bitmap(1920, 1080);
        }


        [Test]
        public void Save_WhenCorrectPath()
        {
            sut.Save(testingBmp, currentDirectory, $"test", ImageFormat.Png);
            IsFileExisting(currentDirectory, "test.png").Should().BeTrue();
        }

        [Test]
        public void CreateDirectory_WhenDirectoryNotExist()
        {
            var directoryPath = Path.Combine(currentDirectory.FullName, "notexistingdirectory");
            var newDirectory = new DirectoryInfo(directoryPath);
            if(newDirectory.Exists)
                newDirectory.Delete(recursive:true);
            newDirectory.Exists.Should().BeFalse();
            sut.Save(testingBmp, new DirectoryInfo(directoryPath), "test", ImageFormat.Png);
            Directory.Exists(directoryPath).Should().BeTrue();
            newDirectory.Delete(recursive:true);
        }

        [Test]
        public void ThrowWithMessage_WhenDisposedBitmap()
        {
            testingBmp.Dispose();
            Action act = () => sut.Save(testingBmp, currentDirectory, "test", ImageFormat.Png);
            act.Should().Throw<Exception>().WithMessage("Не удалось сохранить файл");
        }

        private bool IsFileExisting(DirectoryInfo directory, string fileNameWithExt)
        {
            var file = directory
                .GetFiles()
                .SingleOrDefault(f => f.Name == fileNameWithExt);
            if (file != null)
            {
                file.Delete();
                return true;
            }

            return false;
        }
    }
}