using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudLibrary.MyStem;

namespace TagsCloudLibraryTests
{
    [TestFixture]
    class MyStemProcessTests
    {
        private MyStemProcess myStemProcess;
        [SetUp]
        public void SetUp()
        {
            myStemProcess = new MyStemProcess();
        }

        [Test]
        public void StreamToWords_ExtractsInitialForms()
        {
            var inputStream = StringToStream("Вася быстро шёл по дороге: ему нужно было успеть в университет");
            var expectedWords = new List<string>
            {
                "вася",
                "быстро",
                "идти",
                "по",
                "дорога",
                "он",
                "нужно",
                "быть",
                "успевать",
                "в",
                "университет"
            };

            var words = myStemProcess.StreamToWords(inputStream).ToList();

            words.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void StreamToWords_NoWords_Succeeds()
        {
            var inputStream = StringToStream("");

            var words = myStemProcess.StreamToWords(inputStream);

            words.Should().BeEmpty();
        }

        [Test]
        public void GetWordsWithGrammar_DetectsPartsOfSpeech()
        {
            var wordsList = new List<string>
            {
                "вася",
                "быстро",
                "идти",
                "по",
                "дорога",
                "он",
                "нужно",
                "быть",
                "успевать",
                "в",
                "университет"
            };
            var expectedPartsOfSpeech = new List<Word.PartOfSpeech>
            {
                Word.PartOfSpeech.Noun,
                Word.PartOfSpeech.Adverb,
                Word.PartOfSpeech.Verb,
                Word.PartOfSpeech.Preposition,
                Word.PartOfSpeech.Noun,
                Word.PartOfSpeech.NounPronoun,
                Word.PartOfSpeech.Adverb,
                Word.PartOfSpeech.Verb,
                Word.PartOfSpeech.Verb,
                Word.PartOfSpeech.Preposition,
                Word.PartOfSpeech.Noun,
            };

            var partsOfSpeech = myStemProcess.GetWordsWithGrammar(wordsList)
                .Select(word => word.Grammar.PartOfSpeech).ToList();

            partsOfSpeech.Should().BeEquivalentTo(expectedPartsOfSpeech, 
                config => config.WithStrictOrdering());
        }

        [Test]
        public void GetWordsWithGrammar_StoresBothInputFormAndInitialForm()
        {
            var wordsList = new List<string>
            {
                "вася",
                "быстро",
                "шёл",
                "по",
                "дороге",
                "ему",
                "нужно",
                "было",
                "успеть",
                "в",
                "университет"
            };

            var expectedInitialForms = new List<string>
            {
                "вася",
                "быстро",
                "идти",
                "по",
                "дорога",
                "он",
                "нужно",
                "быть",
                "успевать",
                "в",
                "университет"
            };


            var words = myStemProcess.GetWordsWithGrammar(wordsList);
            var initialStrings = words.Select(word => word.InitialString);
            var initialForms = words.Select(word => word.Grammar.InitialForm);

            initialStrings.Should().BeEquivalentTo(wordsList,
                config => config.WithStrictOrdering());
            initialForms.Should().BeEquivalentTo(expectedInitialForms,
                config => config.WithStrictOrdering());
        }

        [Test]
        public void GetWordsWithGrammar_NoWords_Succeeds()
        {
            var wordsList = new List<string>();

            var wordsWithGrammar = myStemProcess.GetWordsWithGrammar(wordsList);

            wordsWithGrammar.Should().BeEmpty();
        }

        private static Stream StringToStream(string input) => new MemoryStream(Encoding.UTF8.GetBytes(input));
    }
}
