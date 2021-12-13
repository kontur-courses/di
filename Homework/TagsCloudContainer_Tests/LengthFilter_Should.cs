using System;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class LengthFilter_Should
    {
        private readonly WordInfo[] testingInput =
        {
            new("мяч", SpeechPart.Noun),
            new("футбольный", SpeechPart.Adjective),
            new("пинать", SpeechPart.Verb),
            new("ой", SpeechPart.Unknown)
        };

        private readonly ITagCloudSettings settings = A.Fake<ITagCloudSettings>();

        private LengthFilter sut;

        [Test]
        public void ExcludeWords_WhenLengthSpecified()
        {
            A.CallTo(() => settings.MinWordLength).Returns(3);
            sut = new LengthFilter(settings);
            sut.Filter(testingInput)
                .Should()
                .NotContain(w => w.Lemma.Length < 3);
        }

        [Test]
        public void NotExcludeWords_WhenZeroArgument()
        {
            A.CallTo(() => settings.MinWordLength).Returns(0);
            sut = new LengthFilter(settings);
            sut.Filter(testingInput)
                .Should()
                .BeEquivalentTo(testingInput);
        }

        [Test]
        public void ExcludeAllWords_WhenHighLengthRequired()
        {
            A.CallTo(() => settings.MinWordLength).Returns(int.MaxValue);
            sut = new LengthFilter(settings);
            sut.Filter(testingInput)
                .Should()
                .BeEmpty();
        }

        [Test]
        public void Throw_WhenNegativeArgument()
        {
            A.CallTo(() => settings.MinWordLength).Returns(-1);
            Assert.Throws<ArgumentException>(() => sut = new LengthFilter(settings));
        }
    }
}