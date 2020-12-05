using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests.UnitTests
{
    public class WordsFilterShould
    {
        private WordsFilter _sut;
        private ISpeechPartsParser _speechPartsParser;
        private ITextProcessingSettings _textProcessingSettings;
        private ISpeechPartsFilter _speechPartsFilter;

        [SetUp]
        public void SetUp()
        {
            _speechPartsParser = A.Fake<ISpeechPartsParser>();
            _textProcessingSettings = A.Fake<ITextProcessingSettings>();
            _speechPartsFilter = A.Fake<ISpeechPartsFilter>();
            _sut = new WordsFilter(_speechPartsParser, _textProcessingSettings, _speechPartsFilter);
        }

        [Test]
        public void GetInterestingWords_ThrowException_WhenStringIsNull()
        {
            var act = new Action(() => _sut.GetInterestingWords(null));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetInterestingWords_BeNotCalledParseToPartSpeechAndWords_WhenStringIsNull()
        {
            try
            {
                var _ = new Action(() => _sut.GetInterestingWords(null));
            }
            catch
            {
                // ignored
            }

            A.CallTo(() => _speechPartsParser.ParseToPartSpeechAndWords(A<string>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void GetInterestingWords_BeNotCalledGetInterestingSpeechParts_WhenStringIsNull()
        {
            try
            {
                var _ = new Action(() => _sut.GetInterestingWords(null));
            }
            catch
            {
                // ignored
            }

            A.CallTo(() => _speechPartsFilter.GetInterestingSpeechParts(A<string[]>.Ignored)).MustNotHaveHappened();
        }

        [Test]
        public void GetInterestingWords_Words_WhenStringContainsInterestingWords()
        {
            var text = "собака кот в она подвал";
            var words = text.Split(' ');
            A.CallTo(() => _speechPartsParser
                    .ParseToPartSpeechAndWords(A<string>.Ignored))
                .Returns(new Dictionary<string, List<string>>
                {
                    ["S"] = new List<string> {words[0], words[1], words.Last()}, ["PR"] = new List<string> {words[2]},
                    ["APRO"] = new List<string> {words[3]}
                });
            A.CallTo(() => _speechPartsFilter.GetInterestingSpeechParts(A<string[]>.Ignored))
                .Returns(new[] {"S"});
            A.CallTo(() => _textProcessingSettings.BoringWords).Returns(new[] {words.Last()});

            var act = _sut.GetInterestingWords(text);

            act.Should().BeEquivalentTo(words[0], words[1]);
        }
    }
}