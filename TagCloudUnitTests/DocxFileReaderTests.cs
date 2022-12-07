using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using TagCloud;

namespace TagCloudUnitTests
{
    [TestFixture]
    public class DocxFileReaderTests
    {
        private DocxFileReader fileReader;

        [SetUp]
        public void Setup()
        {
            fileReader = new DocxFileReader();
        }

        [Test]
        public void ReadAllText_ReturnsAllFileText_WhenFileExists()
        {
            var expectedText = "This is docx file.";

            var filePath = Path.Combine(Environment.CurrentDirectory, @"TestTextFiles\TestText.docx");

            var actualText = fileReader.ReadAllText(filePath);

            actualText.Should().BeEquivalentTo(expectedText);
        }

        [Test]
        public void ReadAllText_ThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            Action action = () => fileReader.ReadAllText("blablabla");

            action.Should().Throw<FileNotFoundException>();
        }
    }
}
