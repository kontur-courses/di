using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagCloud.Infrastructure;
using TagCloud.WordsProcessing;

namespace TagCloudTests.WordsProcessing
{
    public class WordClassBasedSelectorTests
    {
        private HashSet<WordClass> blackList;
        private HashSet<WordClass> whiteList;
        private IWordClassIdentifier wordClassIdentifier;

        [SetUp]
        public void SetUp()
        {
            blackList = new HashSet<WordClass>{WordClass.Preposition, WordClass.Pronoun};
            whiteList = new HashSet<WordClass>{WordClass.Noun, WordClass.Adjective};
            wordClassIdentifier = Substitute.For<IWordClassIdentifier>();
            wordClassIdentifier.GetWordClass("по").Returns(WordClass.Preposition);
            wordClassIdentifier.GetWordClass("он").Returns(WordClass.Pronoun);
            wordClassIdentifier.GetWordClass("окно").Returns(WordClass.Noun);
            wordClassIdentifier.GetWordClass("холодный").Returns(WordClass.Adjective);
        }


        [Test]
        public void IsSelectedWord_ShouldReturnTrue_OnWordNotInBlackList()
        {
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, blackList);

            wordSelector.IsSelectedWord(new Word("окно")).Should().BeTrue();
        }

        [Test]
        public void IsSelectedWord_ShouldReturnFalse_OnWordInBlackList()
        {
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, blackList);

            wordSelector.IsSelectedWord(new Word("он")).Should().BeFalse();
        }

        [Test]
        public void IsSelectedWord_ShouldReturnTrue_OnWordInWhiteList()
        {
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, whiteList, false);

            wordSelector.IsSelectedWord(new Word("холодный")).Should().BeTrue();
        }

        [Test]
        public void IsSelectedWord_ShouldReturnFalse_OnWordNotInWhiteList()
        {
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, whiteList, false);

            wordSelector.IsSelectedWord(new Word("по")).Should().BeFalse();
        }
    }
}
