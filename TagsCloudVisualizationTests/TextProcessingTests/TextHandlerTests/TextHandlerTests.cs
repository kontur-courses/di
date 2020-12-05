using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.TextProcessing.TextHandler;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualizationTests.TextProcessingTests.TextHandlerTests
{
    public class TextHandlerTests
    {
        [Test]
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain4NonRepeatingWords()
        {
            var expected = new List<Word>
            {
                new Word("hello", 1),
                new Word("world", 1),
                new Word("and", 1),
                new Word("arina", 1)
            };
            
            var result = TextHandler.GetOrderedByFrequencyWords("hello world and Arina");

            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain4RepeatingWords()
        {
            var expected = new List<Word> {new Word("hello", 4)};

            var result = TextHandler.GetOrderedByFrequencyWords("hello hello hello hello");

            result.Should().BeEquivalentTo(expected);
        }
        
                
        [Test]
        public void GetWordsFrequency_ContainWordsInLowerCase_WhenInputContainsWordInUpperCase()
        {
            var expected = new List<Word> {new Word("hello", 1)};

            var result = TextHandler.GetOrderedByFrequencyWords("HELLO");

            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWordsFrequency_NotContainForbiddenSigns()
        {
            var expected = new List<Word> {new Word("hello", 1)};
            
            var result = TextHandler.GetOrderedByFrequencyWords("Hello!,!.+ 21!");

            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWordsFrequency_OrderedByFrequenceDescending()
        {
            var expected = new List<Word>
            {
                new Word("hello", 3),
                new Word("world", 2),
                new Word("all", 1)
            };
            
            var result = TextHandler.GetOrderedByFrequencyWords("Hello hello hello all world world");

            result.Should().BeEquivalentTo(expected);
        }
    }
}