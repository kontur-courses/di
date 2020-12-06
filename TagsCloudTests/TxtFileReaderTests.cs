using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    public class TxtFileReaderTests
    {
        private readonly TxtFileReader reader = new TxtFileReader();

        [Test]
        public void TxtFileReader_ShouldThrow_WithWrongFileType()
        {
            var fileName = @"C:\Users\da\Desktop\abc.jpeg";
            Action action = () => reader.ReadLines(fileName);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void DocFileReader_ShouldReadLines_FromDocTypeFile()
        {
            var fileName = Directory.GetCurrentDirectory() + @"\FileReadersTestsFiles\TxtFileReaderTestFile.txt";
            TestContext.WriteLine(fileName);
            var words = reader.ReadLines(fileName);
            words.Should().BeEquivalentTo("Abc", "Aa", "Abcg", "Def", "Gf");
        }

        [Test]
        public void DocFileReader_ShouldReturnEmptyCollection_FromEmptyDocFile()
        {
            var fileName = Directory.GetCurrentDirectory() + @"\FileReadersTestsFiles\EmptyTxtFile.txt";
            TestContext.WriteLine(fileName);
            var words = reader.ReadLines(fileName);
            words.Should().BeEquivalentTo();
        }
    }
}