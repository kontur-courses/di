using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure;
using TagCloud.WordsProcessing;

namespace TagCloudTests.WordsProcessing
{
    public class WordCounterTests
    {

        [Test]
        public void GetCountedWords_ShouldSetCorrectCount_OnWords()
        {
            var wordCounter = new WordCounter();
            var word1 = new Word("груша");
            var word2 = new Word("яблоко");
            var word3 = new Word("банан");
            var words = new List<Word>
            {
                new Word("груша"),
                new Word("яблоко"),
                new Word("банан"),
                new Word("груша"),
                new Word("груша"),
                new Word("банан")
            };
            var expectedCountedWords = new List<Word> {word3.SetCount(2), word1.SetCount(3), word2.SetCount(1)};

            var result = wordCounter.GetCountedWords(words);

            result.Should().BeEquivalentTo(expectedCountedWords);
        }

    }
}
