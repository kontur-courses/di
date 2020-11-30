using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextHandler;

namespace TagsCloudVisualizationTests.TextHandlerTests
{
    public class TextHandlerTests
    {
        private string pathToTestTexts = @"..\..\..\TestTexts\";
        
        [Test]
        public void GetWordsFrequencyFromFile_Throws_WhenFileNotExists()
        {
            Assert.Throws<IOException>(() => TextHandler.GetWordsFrequencyFromFile("sadas"));
        }
        
        [Test]
        public void GetWordsFrequencyFromFile_ContainWordsCountFromFile_WhenFileContain4WordsWithoutForbiddenSigns()
        {
            var path = @"..\..\..\TestTexts\test1.txt";
            
            var result = TextHandler.GetWordsFrequencyFromFile(path);

            result.Count.Should().Be(4);
        }

        [Test]
        public void GetWordsFrequencyFromFile_ContainWordsFromFile_WhenFileContain4NonRepeatingWords()
        {
            var path = pathToTestTexts + "test1.txt";
            var expected = new Dictionary<string, int>
            {
                {"hello", 1},
                {"world", 1},
                {"and", 1},
                {"arina", 1},
            };
            
            var result = TextHandler.GetWordsFrequencyFromFile(path);

            result.Should().Equal(expected);
        }
        
        [Test]
        public void GetWordsFrequencyFromFile_ContainWordsFromFile_WhenFileContain4RepeatingWords()
        {
            var path = pathToTestTexts + "test2.txt";
            var expected = new Dictionary<string, int> {{"hello", 4}};

            var result = TextHandler.GetWordsFrequencyFromFile(path);

            result.Should().Equal(expected);
        }
        
                
        [Test]
        public void GetWordsFrequencyFromFile_ContainWordsFromFileInLowerCase_WhenFileContainsWordInUpperCase()
        {
            var path = pathToTestTexts + "test3.txt";
            var expected = new Dictionary<string, int> {{"hello", 1}};

            var result = TextHandler.GetWordsFrequencyFromFile(path);

            result.Should().Equal(expected);
        }
        
        [Test]
        public void GetWordsFrequencyFromFile_NotContainForbiddenSigns()
        {
            var path = pathToTestTexts + "test4.txt";
            var expected = new Dictionary<string, int> {{"hello", 1}};
            
            var result = TextHandler.GetWordsFrequencyFromFile(path);

            result.Should().Equal(expected);
        }
    }
}