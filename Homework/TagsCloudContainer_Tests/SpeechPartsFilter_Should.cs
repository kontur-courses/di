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
    public class SpeechPartsFilter_Should
    {
        private readonly WordInfo[] testingInput =
        {
            new("мяч", SpeechPart.Noun),
            new("футбольный", SpeechPart.Adjective),
            new("пинать", SpeechPart.Verb),
            new("ой", SpeechPart.Unknown)
        };

        private readonly ITagCloudSettings settings = A.Fake<ITagCloudSettings>();

        private SpeechPartsFilter sut;

        [Test]
        public void ReturnSpecifiedSpeechPart()
        {
            A.CallTo(() => settings.SelectedSpeechParts).Returns(new[] {SpeechPart.Noun});
            sut = new SpeechPartsFilter(settings);
            sut.Filter(testingInput)
                .Should()
                .OnlyContain(x => x.SpeechPart == SpeechPart.Noun);
        }

        [Test]
        public void ReturnSpecifiedSpeechParts()
        {
            A.CallTo(() => settings.SelectedSpeechParts).Returns(new[] {SpeechPart.Noun, SpeechPart.Adjective});
            sut = new SpeechPartsFilter(settings);
            sut.Filter(testingInput)
                .Should()
                .OnlyContain(w => w.SpeechPart == SpeechPart.Noun || w.SpeechPart == SpeechPart.Adjective);
        }

        [Test]
        public void ExcludeNothing_WhenEmptyFilter()
        {
            A.CallTo(() => settings.SelectedSpeechParts).Returns(Array.Empty<SpeechPart>());
            sut = new SpeechPartsFilter(settings);
            sut.Filter(testingInput)
                .Should()
                .BeEquivalentTo(testingInput);
        }
    }
}