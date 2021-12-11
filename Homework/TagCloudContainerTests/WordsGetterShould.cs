using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TagsCloudContainer;

namespace TagCloudContainerTests
{
    [TestFixture]
    public class WordsGetterShould
    {
        private HashSet<string> boringWords;
        [SetUp]
        public void CreateEmptyBoringWords()
        {
            boringWords = new HashSet<string>();
        }

        [Test]
        public void Throw_FileNotFoundException_WhenPathIsIncorrect()
        {
            string incorrectPath = "words";
            Action wordsGetterCreationWithIncorrectPath =
                () => new WordsGetter(incorrectPath, boringWords);

            wordsGetterCreationWithIncorrectPath.Should().Throw<FileNotFoundException>();
        }

        [Test]
        public void Throw_ArgumentNullException_WhenPathIsNull()
        {
            Action wordsGetterCreationWithIncorrectPath =
                () => new WordsGetter(null, boringWords);

            wordsGetterCreationWithIncorrectPath.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ShowDistinctWordsCountRight()
        {
            var path = @"D:\di\Homework\TagsCloudContainer\words.txt";
            boringWords = new HashSet<string>() { "I", "" };

            var wordsGetter = new WordsGetter(path, boringWords);
            var distinctWordsCount = wordsGetter.GetDistinctWordsCount();

            distinctWordsCount.Should().Be(19);
        }
    }
}
