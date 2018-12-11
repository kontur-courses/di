using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility;
using TagCloud.Utility.Container;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    public class TagCloudProgram_Should
    {
        [Test]
        public void ThrowArgumentException_WhenOptionsAreNull()
        {
            Action execute = () => TagCloudProgram.Execute(null);

            execute.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ThrowArgumentException_WhenFilesDoesNotExists()
        {
            Action execute = () => TagCloudProgram.Execute(Options.Standart);

            execute.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CreatePicture()
        {
            using (var file = File.CreateText("text.txt"))
                file.WriteLine("some text here 123 just for test");
            var options = Options.Standart;
            options.PathToPicture = "result.png";
            options.PathToWords = "text.txt";

            TagCloudProgram.Execute(Options.Standart);

            File.Exists("result.png").Should().BeTrue();
            File.Delete("result.png");
            File.Delete("text.txt");
        }

        [Test]
        public void ChangeImageSize()
        {
            using (var file = File.CreateText("text.txt"))
                file.WriteLine("some text here 123 just for test");
            var options = Options.Standart;
            options.Size = "100x100";
            options.PathToPicture = "result.png";
            options.PathToWords = "text.txt";

            TagCloudProgram.Execute(Options.Standart);

            using (var image = Image.FromFile("result.png"))
                image.Size.Should().Be(new Size(100, 100));
            File.Delete("result.png");
            File.Delete("text.txt");
        }
    }
}