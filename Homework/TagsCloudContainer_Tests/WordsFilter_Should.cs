using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordsFilter;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer_Tests
{
    [TestFixture]
    public class WordsFilterAnalyzer_Should
    {
        private readonly WordInfo[] testingInput = new[]
        {
            new WordInfo("мяч", SpeechPart.Noun),
            new WordInfo("футбольный", SpeechPart.Adjective),
            new WordInfo("пинать", SpeechPart.Verb),
            new WordInfo("ой", SpeechPart.Unknown),
        };
        
        private WordsFilter sut;
        
        [SetUp]
        public void SetUp()
        {
            sut = new WordsFilter();
        }

        [Test]
        public void ExcludeSpeechParts_WhenSpecified()
        {
            sut.Excluding(SpeechPart.Noun)
                .Filter(testingInput)
                .Should()
                .NotContain("мяч");
        }

        [Test]
        public void ExcludeWordsWithLengthLessOrEqualTwo_WhenNotOverriden()
        {
            sut.Filter(testingInput).Should().NotContain("ой");
        }

        [Test]
        public void ExcludeCustomBoringWords_WhenOverriden()
        {
            sut.Excluding(wi => wi.Lemma[0] == 'ф')
                .Filter(testingInput)
                .Should()
                .NotContain("футбольный");
        }

        [Test]
        public void BeImmutable()
        {
            sut.Excluding(SpeechPart.Noun)
                .Filter(testingInput);
            
            sut.Filter(testingInput)
                .Should()
                .Contain("мяч");
        }

    }
}