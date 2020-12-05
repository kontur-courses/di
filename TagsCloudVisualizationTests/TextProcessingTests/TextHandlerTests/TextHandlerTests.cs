using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextProcessing.TextHandler;

namespace TagsCloudVisualizationTests.TextProcessingTests.TextHandlerTests
{
    public class TextHandlerTests
    {
        [Test]
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain4NonRepeatingWords()
        {
            var expected = new Dictionary<string, int>
            {
                {"hello", 1},
                {"world", 1},
                {"and", 1},
                {"arina", 1},
            };
            
            var result = TextHandler.GetWordsFrequency("hello world and Arina");

            result.Should().Equal(expected);
        }
        
        [Test]
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain4RepeatingWords()
        {
            var expected = new Dictionary<string, int> {{"hello", 4}};

            var result = TextHandler.GetWordsFrequency("hello hello hello hello");

            result.Should().Equal(expected);
        }
        
                
        [Test]
        public void GetWordsFrequency_ContainWordsInLowerCase_WhenInputContainsWordInUpperCase()
        {
            var expected = new Dictionary<string, int> {{"hello", 1}};

            var result = TextHandler.GetWordsFrequency("HELLO");

            result.Should().Equal(expected);
        }
        
        [Test]
        public void GetWordsFrequencyFromFile_NotContainForbiddenSigns()
        {
            var expected = new Dictionary<string, int> {{"hello", 1}};
            
            var result = TextHandler.GetWordsFrequency("Hello!,!.+ 21!");

            result.Should().Equal(expected);
        }
    }
}