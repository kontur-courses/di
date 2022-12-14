using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.WordReaders;

namespace TagsCloudContainer.Tests.TextFileReaderTests
{
    [TestFixture]
    internal class TextFileReaderShould
    {
        private IWordReader wordReader;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wordReader = TextFileReaderFactory.CreateDefault();
        }

        [Test]
        public void ReturnNegativeResult_WhenFileDoesntExist()
        {
            var result = wordReader.TryReadWords("0", out _);

            result.Success.Should().BeFalse();
        }

        [Test]
        public void ReturnNegativeResult_WhenFileHasWrongExtension()
        {
            var file = File.Create("123");
            var result = wordReader.TryReadWords(file.Name, out _);

            result.Success.Should().BeFalse();
        }

        [Test]
        public void ReadEmptyFile()
        {
            string filename;
            var path = Path.GetTempFileName();
            File.Move(path, filename = Path.ChangeExtension(path, TextFileReaderFactory.FileExtension));

            var result = wordReader.TryReadWords(filename, out var words);
            
            result.Success.Should().BeTrue();
            words.Should().BeEmpty();
        }

        [Test]
        public void ReadWords()
        {
            string filename;
            var path = Path.GetTempFileName();
            File.Move(path, filename = Path.ChangeExtension(path, TextFileReaderFactory.FileExtension));
            var expectedWords = new string[] { "word1", "word2" };
            File.WriteAllLines(filename, expectedWords);

            var result = wordReader.TryReadWords(filename, out var actualWords);

            result.Success.Should().BeTrue();
            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }

    internal static class TextFileReaderFactory
    {
        public static IWordReader CreateDefault() => new TextFileReader();
        public const string FileExtension = "txt";
    }
}