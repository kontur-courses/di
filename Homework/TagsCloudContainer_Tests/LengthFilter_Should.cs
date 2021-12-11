using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class LengthFilter_Should
    {
        private readonly WordInfo[] testingInput =
        {
            new WordInfo("мяч", SpeechPart.Noun),
            new WordInfo("футбольный", SpeechPart.Adjective),
            new WordInfo("пинать", SpeechPart.Verb),
            new WordInfo("ой", SpeechPart.Unknown)
        };
        private LengthFilter sut;

        [Test]
        public void ExcludeWords_WhenLengthSpecified()
        {
            sut = new LengthFilter(3);
            sut.Filter(testingInput)
                .Should()
                .NotContain(w => w.Lemma.Length < 3);
        }

        [Test]
        public void NotExcludeWords_WhenZeroArgument()
        {
            sut = new LengthFilter(0);
            sut.Filter(testingInput)
                .Should()
                .BeEquivalentTo(testingInput);
        }

        [Test]
        public void ExcludeAllWords_WhenHighLengthRequired()
        {
            sut = new LengthFilter(int.MaxValue);
            sut.Filter(testingInput)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Throw_WhenNegativeArgument()
        {
            Assert.Throws<ArgumentException>(() => sut = new LengthFilter(-1));
        }
    }
}