using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using TagCloud.FileReader;

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

            var actualText = fileReader.ReadAllText(@"TestTextFiles\TestText.docx");

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
