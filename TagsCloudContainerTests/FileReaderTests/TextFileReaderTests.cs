using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.Settings;

namespace TagsCloudContainerTests.FileReaderTests
{
    [TestFixture]
    public class TextFileReaderTests
    {
        [Test]
        public void Read_Should_ReturnAllTextInFile()
        {
            var fileSettings =
                new FileSettings(@"C:\Users\gosha\Desktop\di\TagsCloudContainerTests\FileReaderTests\file.txt");
            var textReader = new TextFileReader(fileSettings);
            var expectedResult =
                File.ReadAllText(@"C:\Users\gosha\Desktop\di\TagsCloudContainerTests\FileReaderTests\file.txt");

            var result = textReader.Read();

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}