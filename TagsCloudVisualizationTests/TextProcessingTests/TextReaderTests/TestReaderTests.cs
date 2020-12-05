using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TextReader = TagsCloudVisualization.TextProcessing.TextReader.TextReader;

namespace TagsCloudVisualizationTests.TextProcessingTests.TextReaderTests
{
    public class TestReaderTests
    {
        [Test]
        public void ReadAllText_Throws_WhenTxtFileNotExists()
        {
            Assert.Throws<IOException>(() => TextReader.ReadAllText("sadas"));
        }

        [Test]
        public void ReadAllText_ContainWordsCountFromFile_WhenTxtFileContain4WordsWithoutForbiddenSigns()
        {
            var path = @"..\..\..\TestTexts\test1.txt";
            
            var result = TextReader.ReadAllText(path);

            result.Split(' ').Length.Should().Be(4);
        }
        
        [Test]
        public void ReadAllText_ContainWordsFromTxtFile()
        {
            var path = @"..\..\..\TestTexts\test1.txt";
            
            var result = TextReader.ReadAllText(path);

            result.Should().Be("hello world and Arina");
        }
        
        [Test]
        public void ReadAllText_ContainWordsFromDocxFile()
        {
            var path = @"..\..\..\TestTexts\test2.docx";
            
            var result = TextReader.ReadAllText(path);

            result.Should().Be("Hello world with this beautiful text!");
        }
        
        [Test]
        public void ReadAllText_ContainWordsFromDocFile()
        {
            var path = @"..\..\..\TestTexts\test3.doc";
            
            var result = TextReader.ReadAllText(path);

            result.Should().Be("Hello world with this beautiful text!");
        }
    }
}