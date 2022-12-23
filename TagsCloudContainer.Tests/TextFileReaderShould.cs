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
        public void ReturnFailedResult_WhenFileDoesntExist()
        {
            var result = wordReader.TryReadWords("0");

            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public void ReturnFailedResult_WhenFileHasWrongExtension()
        {
            var file = File.Create("123");
            var result = wordReader.TryReadWords(file.Name);

            result.IsFailed.Should().BeTrue();
        }

        [Test]
        public void ReadEmptyFile()
        {
            string filename;
            var path = Path.GetTempFileName();
            File.Move(path, filename = Path.ChangeExtension(path, TextFileReaderFactory.FileExtension));

            var result = wordReader.TryReadWords(filename);
            
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
        }

        [Test]
        public void ReadWords()
        {
            string filename;
            var path = Path.GetTempFileName();
            File.Move(path, filename = Path.ChangeExtension(path, TextFileReaderFactory.FileExtension));
            var expectedWords = new string[] { "word1", "word2" };
            File.WriteAllLines(filename, expectedWords);

            var result = wordReader.TryReadWords(filename);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expectedWords);
        }
    }

    internal static class TextFileReaderFactory
    {
        public static IWordReader CreateDefault() => new TextFileReader();
        public const string FileExtension = "txt";
    }
}