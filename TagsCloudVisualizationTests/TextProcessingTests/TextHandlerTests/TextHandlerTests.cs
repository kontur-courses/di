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
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain3NonRepeatingWords()
        {
            var expected = new List<Word>
            {
                new Word("hello", 1),
                new Word("world", 1),
                new Word("annd", 1),
                new Word("arina", 1)
            };

            var result = TextHandler.GetOrderedByFrequencyWords("hello world annd Arina");

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
                new Word("good", 1)
            };

            var result = TextHandler.GetOrderedByFrequencyWords("Hello hello hello good world world");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ExcludedShortWords_WhenInputContains1ShortWord()
        {
            var expected = new List<Word>
            {
                new Word("hello", 1),
                new Word("world", 1)
            };

            var result = TextHandler.GetOrderedByFrequencyWords("Hello all world");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ExcludedForbiddenWords_WhenExistsOneForbiddenWord()
        {
            var expected = new List<Word>
            {
                new Word("some", 1),
                new Word("text", 1)
            };

            var result = TextHandler.GetOrderedByFrequencyWords(
                "Some beautiful text",
                new HashSet<string>
                {
                    "beautiful"
                });

            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWordsFrequency_ExcludedForbiddenWords_WhenExistsMoreThanOneForbiddenWord()
        {
            var expected = new List<Word>
            {
                new Word("text", 1)
            };

            var result = TextHandler.GetOrderedByFrequencyWords(
                "Some beautiful text",
                new HashSet<string>
                {
                    "beautiful",
                    "some"
                });

            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWordsFrequency_ExcludedAllWords_WhenForbiddenWordsCountEqualTextWordsCount()
        {
            var expected = new List<Word>();

            var result = TextHandler.GetOrderedByFrequencyWords(
                "Some beautiful text",
                new HashSet<string>
                {
                    "beautiful",
                    "some",
                    "text"
                });

            result.Should().BeEquivalentTo(expected);
        }
    }
}