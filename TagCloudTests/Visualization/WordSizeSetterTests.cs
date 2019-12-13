using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure;
using TagCloud.Visualization;

namespace TagCloudTests.Visualization
{
    public class WordSizeSetterTests
    {
        private WordSizeSetter wordSizeSetter;
        private List<Word> words;
        private Word word1;
        private float word1MinFontSize;
        private Word word2;
        private float word2MinFontSize;
        private Word word3;
        private float word3MinFontSize;
        private PictureConfig pictureConfig;

        [SetUp]
        public void SetUp()
        {
            wordSizeSetter = new WordSizeSetter();
            word1 = new Word("кот").SetCount(1);
            word2 = new Word("пёс").SetCount(3);
            word3 = new Word("мышь").SetCount(5);
            words = new List<Word> { word1, word2, word3 };
            pictureConfig = new PictureConfig();
            word1MinFontSize = 1f / 5;
            word2MinFontSize = 3f / 5;
            word3MinFontSize = 5f / 5;
        }


        [Test]
        public void GetSizedWords_ShouldReturnMinimalSize_OnSmallPictureSize()
        {
            var expectedSizes = new List<Size>
            {
                SizeUtils.GetWordBasedSize(word1.Value, pictureConfig.FontFamily, word1MinFontSize),
                SizeUtils.GetWordBasedSize(word2.Value, pictureConfig.FontFamily, word2MinFontSize),
                SizeUtils.GetWordBasedSize(word3.Value, pictureConfig.FontFamily, word3MinFontSize)
            };
            pictureConfig.Size = new Size(20, 20);
            var result = wordSizeSetter.GetSizedWords(words, pictureConfig).ToList();

            result.Select(w => w.WordRectangleSize).Should().BeEquivalentTo(expectedSizes);
        }

        [Test]
        public void GetSizedWords_ShouldReturnScaledSize_OnBigPictureSize()
        {
            var expectedSizes = new List<Size>
            {
                SizeUtils.GetWordBasedSize(word1.Value, pictureConfig.FontFamily, word1MinFontSize * 14),
                SizeUtils.GetWordBasedSize(word2.Value, pictureConfig.FontFamily, word2MinFontSize * 14),
                SizeUtils.GetWordBasedSize(word3.Value, pictureConfig.FontFamily, word3MinFontSize * 14)
            };
            pictureConfig.Size = new Size(200, 100);
            var result = wordSizeSetter.GetSizedWords(words, pictureConfig);

            result.Select(w => w.WordRectangleSize).Should().BeEquivalentTo(expectedSizes);
        }

        [Test]
        public void GetSizedWords_ShouldThrow_OnTooSmallPictureSize()
        {
            pictureConfig.Size = new Size(15, 6);
            Action action = () => wordSizeSetter.GetSizedWords(words, pictureConfig).ToList();

            action.Should().Throw<ArgumentException>();
        }
    }
}
