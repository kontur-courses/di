using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.Tools;

namespace TagsCloudGeneratorTests.ToolsTests
{
    public class ParseHelperTests
    {
        [Test]
        public void GetWordToCount_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            List<string> words = null;

            Action act = () => ParseHelper.GetWordToCount(words);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestCase(5)]
        [TestCase(10)]
        [TestCase(31)]
        public void GetWordToCount_NWordsToMatch_ShouldReturnDictionaryContainsThisWordAndNCount(int count)
        {
            var word = "text";
            var words = new List<string> {"doctor", "tea", "full", word, "rabbit"};

            for (var i = 0; i < count - 1; i++)
            {
                words.Add(word);
            }

            var wordToCount = ParseHelper.GetWordToCount(words);
            var actual = wordToCount[word];

            actual.Should().Be(count);
        }

        [Test]
        public void GetWordToCount_AllWordsDifferent_ShouldReturnDictionaryContainsThisWordsWithCount1()
        {
            var words = new List<string> {"doctor", "doctors", "tea", "rabbit",};
            var expected = new Dictionary<string, int> {["doctor"] = 1, ["doctors"] = 1, ["tea"] = 1, ["rabbit"] = 1};

            var actual = ParseHelper.GetWordToCount(words);

            actual.Should().BeEquivalentTo(expected);
        }

    }
}