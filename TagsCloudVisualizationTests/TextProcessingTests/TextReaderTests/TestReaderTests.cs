using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TextReader = TagsCloudVisualization.TextProcessing.TextReader.TextReader;

namespace TagsCloudVisualizationTests.TextProcessingTests.TextReaderTests
{
    public class TestReaderTests
    {
        [Test]
        public void ReadAllText_Throws_WhenFileNotExists()
        {
            Assert.Throws<IOException>(() => TextReader.ReadAllText("sadas"));
        }
        
        [Test]
        public void ReadAllText_ContainWordsCountFromFile_WhenFileContain4WordsWithoutForbiddenSigns()
        {
            var path = @"..\..\..\TestTexts\test1.txt";
            
            var result = TextReader.ReadAllText(path);

            result.Split(' ').Length.Should().Be(4);
        }
        
        [Test]
        public void ReadAllText_ContainWordsFromFile()
        {
            var path = @"..\..\..\TestTexts\test1.txt";
            
            var result = TextReader.ReadAllText(path);

            result.Should().Be("hello world and Arina");
        }
    }
}