using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests.UnitTests
{
    public class WordTagsLayouterShould
    {
        private WordTagsLayouter _sut;
        private IWordsFrequency _wordsFrequency;
        private ICloudLayouter _cloudLayouter;
        private IWordMeasurer _wordMeasurer;
        private static readonly Font _font = new Font("arial", 5);

        [SetUp]
        public void SetUp()
        {
            _wordsFrequency = A.Fake<IWordsFrequency>();
            _cloudLayouter = A.Fake<ICloudLayouter>();
            _wordMeasurer = A.Fake<IWordMeasurer>();
            _sut = new WordTagsLayouter(_wordsFrequency, _cloudLayouter, _wordMeasurer, _font);
        }

        [Test]
        public void GetWordTags_ThrowException_WhenStringIsNotNull()
        {
            var act = new Action(() =>
            {
                var wordTags = _sut.GetWordTags(null).ToList();
            });

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordTags_BeNotCalledAnyMethodDependencies_WhenStringIsNull()
        {
            try
            {
                var wordTags = _sut.GetWordTags(null).ToList();
            }
            catch
            {
                // ignored
            }

            A.CallTo(() => _wordsFrequency.GetWordsFrequency(A<string>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _wordMeasurer.GetWordSize(A<string>.Ignored, A<Font>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _cloudLayouter.PutNextRectangle(A<Size>.Ignored)).MustNotHaveHappened();
        }

        [TestCase("")]
        [TestCase("игра")]
        public void GetWordTags_BeCalledGetWordsFrequencyOnce_WhenStringIsNotNull(string text)
        {
            var act = _sut.GetWordTags(text).ToList();

            A.CallTo(() => _wordsFrequency.GetWordsFrequency(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GetWordTags_BeCalledGetWordSizeCertainNumber_WhenStringIsNotNull()
        {
            var text = "игра теннис";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFrequency.GetWordsFrequency(text)).Returns(new Dictionary<string, int>
                {[words[0]] = 1, [words[1]] = 1});

            var act = _sut.GetWordTags(text).ToList();

            A.CallTo(() => _wordMeasurer.GetWordSize(A<string>.Ignored, A<Font>.Ignored))
                .MustHaveHappened(text.Split(' ').Length, Times.Exactly);
        }

        [Test]
        public void GetWordTags_BeCalledPutNextRectangleCertainNumber_WhenStringIsNotNull()
        {
            var text = "игра теннис";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFrequency.GetWordsFrequency(text)).Returns(new Dictionary<string, int>
                {[words[0]] = 1, [words[1]] = 1});

            var act = _sut.GetWordTags(text).ToList();

            A.CallTo(() => _cloudLayouter.PutNextRectangle(A<Size>.Ignored))
                .MustHaveHappened(text.Split(' ').Length, Times.Exactly);
        }

        [Test]
        public void GetWordTags_CertainNumberTags_WhenStringIsNotNull()
        {
            var text = "игра теннис";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFrequency.GetWordsFrequency(text)).Returns(new Dictionary<string, int>
                {[words[0]] = 1, [words[1]] = 1});

            var act = _sut.GetWordTags(text);

            act.Should().HaveCount(words.Length);
        }
    }
}