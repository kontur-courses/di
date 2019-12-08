using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.WordsPreparation;

namespace TagCloudTests
{
    public class WordWeightSetterTests
    {

        [Test]
        public void GetCountedWords_ShouldSetCorrectCount_OnWords()
        {
            var wordWeightSetter = new WordCountSetter();
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
            var expectedCountedWords = new List<Word> {word3.WithCount(2), word1.WithCount(3), word2.WithCount(1)};

            var result = wordWeightSetter.GetCountedWords(words);

            result.Should().BeEquivalentTo(expectedCountedWords);
        }

    }
}
