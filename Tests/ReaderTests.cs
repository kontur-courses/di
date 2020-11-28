using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using WordCloudGenerator;

namespace Tests
{
    public class ReaderTests
    {
        private Reader reader;
        
        [SetUp]
        public void SetUp()
        {
            reader = new Reader();
        }
        
        [Test]
        public void ReadFile_ShouldReturnPlainText_ExistingFile()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "../../..", "testFile.txt");
            reader.ReadFile(path).Should().Be("abc\r\nabc\r\nefg");
        }

        [Test]
        public void ReadFile_ShouldThrow_NotExistingFile()
        {
            var func = new Action(() => reader.ReadFile("absolutely not a file path"));

            func.Should().Throw<FileNotFoundException>();
        }
    }
}