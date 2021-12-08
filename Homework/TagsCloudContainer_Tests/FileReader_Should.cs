using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TextReader;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class FileReader_Should
    {
        private const string testFilePath = @"..\..\..\TestFiles\testWords.txt";

        private readonly string[] expectedOutput =
        {
            "гитара",
            "скрипка",
            "  Инструмент",
            "Был",
            "Удар",
            "Я",
            "Играл",
            "Смотрел",
            "снова"
        };

        private readonly FileReader sut = new();

        [Test]
        public void ReadFile_WhenExists()
        {
            var result = sut.ReadWords(testFilePath);
            result.Should().BeEquivalentTo(expectedOutput);
        }

        [Test]
        public void Throw_WhenFileDoesNotExist()
        {
            Assert.Throws<Exception>(() => sut.ReadWords("notexist.txt").First());
        }

        [Test]
        public void ReturnNothing_WhenEmptyFile()
        {
            sut.ReadWords(@"..\..\..\TestFiles\empty.txt").Should().BeEmpty();
        }
    }
}