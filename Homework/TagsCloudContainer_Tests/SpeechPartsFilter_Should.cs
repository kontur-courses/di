using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsConverters;
using TagsCloudContainer.WordsFilter;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class SpeechPartsFilter_Should
    {
        private readonly WordInfo[] testingInput =
        {
            new WordInfo("мяч", SpeechPart.Noun),
            new WordInfo("футбольный", SpeechPart.Adjective),
            new WordInfo("пинать", SpeechPart.Verb),
            new WordInfo("ой", SpeechPart.Unknown)
        };

        private SpeechPartsFilter sut;

        [Test]
        public void ReturnSpecifiedSpeechPart()
        {
            sut = new SpeechPartsFilter(SpeechPart.Noun);
            sut.Filter(testingInput)
                .Should()
                .OnlyContain(x => x.SpeechPart == SpeechPart.Noun);
        }

        [Test]
        public void ReturnSpecifiedSpeechParts()
        {
            sut = new SpeechPartsFilter(SpeechPart.Noun, SpeechPart.Adjective);
            sut.Filter(testingInput)
                .Should()
                .OnlyContain(w => w.SpeechPart == SpeechPart.Noun || w.SpeechPart == SpeechPart.Adjective);
        }

        [Test]
        public void ExcludeNothing_WhenEmptyFilter()
        {
            sut = new SpeechPartsFilter();
            sut.Filter(testingInput)
                .Should()
                .BeEquivalentTo(testingInput);
        }
    }
}