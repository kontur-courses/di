using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using TagCloud.FileReader;

namespace TagCloudUnitTests
{

    public class TxtFileReaderTests
    {
        private TxtFileReader fileReader;

        [SetUp]
        public void Setup()
        {
            fileReader = new TxtFileReader();
        }

        [Test]
        public void ReadAllText_ReturnsAllFileText_WhenFileExists()
        {
            var expectedText = "This is txt file.";

            var actualText = fileReader.ReadAllText(@"TestTextFiles\TestText.txt");

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
