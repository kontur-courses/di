using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace TagsCloudContainer.Words
{
    [TestFixture]
    public class WordPreprocessing_Should
    {
        private WordPreprocessing wordPreprocessing;
        private string[] words;

        [SetUp]
        public void SetUp()
        {
            words = new string[] { "3", "3", "3", "2", "2", "1", "A", "a", "aAa", "подсолнух", "трава" };
            wordPreprocessing = new WordPreprocessing(words);
        }

        [Test]
        public void ToLower_Should()
        {
            wordPreprocessing.ToLower();

            var newWords = wordPreprocessing.Words.ToArray();
            newWords.Length.Should().Be(words.Length);
            newWords[6].Should().Be("a");
            newWords[8].Should().Be("aaa");
            newWords[9].Should().Be("подсолнух");
        }

        [Test]
        public void IgnoreInvalidWords_Should()
        {
            wordPreprocessing.IgnoreInvalidWords();

            var newWords = wordPreprocessing.Words.ToArray();
            newWords.Length.Should().Be(2);
            newWords[0].Should().Be("подсолнух");
            newWords[1].Should().Be("трава");
        }

        [Test]
        public void Exclude_Should()
        {
            var wordsToExclude = new HashSet<string>() { "1", "2", "3" };

            wordPreprocessing.Exclude(wordsToExclude);

            var newWords = wordPreprocessing.Words.ToArray();
            newWords.Length.Should().Be(5);
            newWords[0].Should().Be("A");
            newWords[1].Should().Be("a");
        }

        [Test]
        public void CustomPreprocessingWhere_Should()
        {
            bool func(string s) => int.TryParse(s, out _);

            wordPreprocessing.CustomPreprocessingWhere(func);

            var newWords = wordPreprocessing.Words.ToArray();
            newWords.Length.Should().Be(6);
            newWords.All(w => int.TryParse(w, out _)).Should().BeTrue();
        }

        [Test]
        public void CustomPreprocessingSelect_Should()
        {
            string func(string s) => s + "ый";

            wordPreprocessing.CustomPreprocessingSelect(func);

            var newWords = wordPreprocessing.Words.ToArray();
            newWords.Length.Should().Be(words.Length);
            newWords.All(w => w.EndsWith("ый")).Should().BeTrue();
        }
    }

}
