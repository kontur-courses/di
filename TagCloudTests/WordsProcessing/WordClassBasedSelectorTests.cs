using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagCloud.App;
using TagCloud.Infrastructure;
using TagCloud.WordsProcessing;

namespace TagCloudTests.WordsProcessing
{
    public class WordClassBasedSelectorTests
    {
        private HashSet<WordClass> blackList;
        private HashSet<WordClass> whiteList;
        private IWordClassIdentifier wordClassIdentifier;
        private ISettingsProvider settingsProvider;
        private AppSettings settings;

        [SetUp]
        public void SetUp()
        {
            settings = new AppSettings();
            blackList = new HashSet<WordClass>{WordClass.Preposition, WordClass.Pronoun};
            whiteList = new HashSet<WordClass>{WordClass.Noun, WordClass.Adjective};
            wordClassIdentifier = Substitute.For<IWordClassIdentifier>();
            wordClassIdentifier.GetWordClass("по").Returns(WordClass.Preposition);
            wordClassIdentifier.GetWordClass("он").Returns(WordClass.Pronoun);
            wordClassIdentifier.GetWordClass("окно").Returns(WordClass.Noun);
            wordClassIdentifier.GetWordClass("холодный").Returns(WordClass.Adjective);
            settingsProvider = Substitute.For<ISettingsProvider>();
            settingsProvider.GetSettings().Returns(settings);
        }


        [Test]
        public void IsSelectedWord_ShouldReturnTrue_OnWordNotInBlackList()
        {
            settings.WordClassSettings = new WordClassSettings(blackList, true);
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, settingsProvider);

            wordSelector.IsSelectedWord(new Word("окно")).Should().BeTrue();
        }

        [Test]
        public void IsSelectedWord_ShouldReturnFalse_OnWordInBlackList()
        {
            settings.WordClassSettings = new WordClassSettings(blackList, true);
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, settingsProvider);

            wordSelector.IsSelectedWord(new Word("он")).Should().BeFalse();
        }

        [Test]
        public void IsSelectedWord_ShouldReturnTrue_OnWordInWhiteList()
        {
            settings.WordClassSettings = new WordClassSettings(whiteList, false);
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, settingsProvider);

            wordSelector.IsSelectedWord(new Word("холодный")).Should().BeTrue();
        }

        [Test]
        public void IsSelectedWord_ShouldReturnFalse_OnWordNotInWhiteList()
        {
            settings.WordClassSettings = new WordClassSettings(whiteList, false);
            var wordSelector = new WordClassBasedSelector(wordClassIdentifier, settingsProvider);

            wordSelector.IsSelectedWord(new Word("по")).Should().BeFalse();
        }
    }
}
