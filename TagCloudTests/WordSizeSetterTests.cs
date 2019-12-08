using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagCloud.WordsPreparation;

namespace TagCloudTests
{
    public class WordSizeSetterTests
    {
        private WordSizeSetter wordSizeSetter;
        private List<Word> words;
        private Word word1;
        private Word word2;
        private Word word3;

        [SetUp]
        public void SetUp()
        {
            wordSizeSetter = new WordSizeSetter(new WordCountSetter(), 5);
            word1 = new Word("кот");
            word2 = new Word("пёс");
            word3 = new Word("мышь");
            words = new List<Word> { word1, word2, word2, word2, word3, word3, word3, word3, word3 };
        }


        [Test]
        public void GetSizedWords_ShouldReturnMinimalSize_OnSmallPictureSize()
        {
            var expectedWords = new List<Word>
            {
                word1.WithCount(1).WithSize(new Size(3, 1)),
                word2.WithCount(3).WithSize(new Size(9, 3)),
                word3.WithCount(5).WithSize(new Size(20, 5))
            };

            var result = wordSizeSetter.GetSizedWords(words, new Size(40, 20));

            result.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void GetSizedWords_ShouldReturnScaledSize_OnBigPictureSize()
        {
            var expectedWords = new List<Word>
            {
                word1.WithCount(1).WithSize(new Size(15, 5)),
                word2.WithCount(3).WithSize(new Size(45, 15)),
                word3.WithCount(5).WithSize(new Size(100, 25))
            };

            var result = wordSizeSetter.GetSizedWords(words, new Size(200, 100));

            result.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void GetSizedWords_ShouldThrow_OnTooSmallPictureSize()
        {
            Action action = () => wordSizeSetter.GetSizedWords(words, new Size(30, 20)).ToList();

            action.Should().Throw<ArgumentException>();
        }
    }
}
