using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.IReaders;

namespace TagCloudTests
{
    public class TextFileReaderTests
    {
        private readonly Random random = new Random();
        private string data;

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [SetUp]
        public void PrepareRandomString()
        {
            data = RandomString(random.Next(100, 1000));
        }

        [Test]
        public void TextFileReader_Read_Data()
        {
            var path = "TextFileData.txt";
            CreateTextFileWithData(path);

            var fileReader = new TextFileReader(path);

            CheckThatDataIsReadBy(fileReader);
            DeleteFile(path);
        }

        private void CreateTextFileWithData(string path)
        {
            File.Exists(path).Should().BeFalse();
            using (var file = new StreamWriter(path))
            {
                file.Write(data);
            }
            File.Exists(path).Should().BeTrue();
        }

        private void CheckThatDataIsReadBy(IReader reader)
        {
            reader.Read().Should().BeEquivalentTo(data);
        }

        private void DeleteFile(string path)
        {
            File.Delete(path);
            File.Exists(path).Should().BeFalse();
        }
    }
}
