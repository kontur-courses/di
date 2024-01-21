using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Readers;

namespace TagsCloudContainerTests
{

    [TestFixture]
    public class FileReaderTests
    {
        private IFileReader fileReader;

        [SetUp]
        public void SetUp()
        {
            fileReader = new TxtReader();
        }

        [Test]
        [TestCase("text.txt")]
        [TestCase("text.docx")]
        [TestCase("text.doc")]
        public void ValidFilePath_ReturnsNonEmptyList(string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var relativeFilePath = Path.Combine("src", fileName);
            var filePath = Path.Combine(currentDirectory, relativeFilePath);

            IFileReader fileReader = GetFileReader(filePath);

            var words = fileReader.ReadWords(filePath);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            words.Should().NotBeNull().And.NotBeEmpty();
        }

        private IFileReader GetFileReader(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".docx", StringComparison.OrdinalIgnoreCase))
            {
                return new DocxReader();
            }
            else if (Path.GetExtension(filePath).Equals(".doc", StringComparison.OrdinalIgnoreCase))
            {
                return new DocReader();
            }
            else
            {
                return new TxtReader();
            }
        }

        [Test]
        public void InvalidFilePath_ReturnsEmptyList()
        {
            var filePath = "path/to/invalid/file.txt";

            var words = fileReader.ReadWords(filePath);

            words.Should().NotBeNull().And.BeEmpty();
        }
    }
}
