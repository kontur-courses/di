using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainerTests
{
    public class WordsFrequencyShould
    {
        private string[] _differentWords = {"кот", "код", "игра"};
        private string[] _existsSameWords = {"тест", "лев", "лев"};

        [Test, TestCaseSource(nameof(TestData_NullOrEmptyArrays))]
        public void GetWordsFrequency_ThrowException_WhenArrayIsNullOrEmpty(string[] words)
        {
            var act = new Action(() => words.GetWordsFrequency());

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordsFrequency_NotException_WhenContainsWords()
        {
            var act = new Action(() => _differentWords.GetWordsFrequency());

            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void GetWordsFrequency_SameCountKeys_WhenDifferentWords()
        {
            var act = _differentWords.GetWordsFrequency();

            act.Should().HaveCount(_differentWords.Length);
        }

        [Test]
        public void GetWordsFrequency_LessWordsKeysNumber_WhenExistsSameWords()
        {
            var act = _existsSameWords.GetWordsFrequency();

            act.Should().HaveCountLessThan(_existsSameWords.Length);
        }

        [Test]
        public void GetWordsFrequency_WordsFrequency_WhenDifferentWords()
        {
            var act = _differentWords.GetWordsFrequency();

            act.Should().BeEquivalentTo(new Dictionary<string, int> {["кот"] = 1, ["код"] = 1, ["игра"] = 1});
        }

        [Test]
        public void GetWordsFrequency_WordsFrequency_WhenExistsSameWords()
        {
            var act = _existsSameWords.GetWordsFrequency();

            act.Should().BeEquivalentTo(new Dictionary<string, int> {["тест"] = 1, ["лев"] = 2});
        }

        private static IEnumerable<string[]> TestData_NullOrEmptyArrays()
        {
            yield return null;
            yield return new string[0];
        }
    }
}