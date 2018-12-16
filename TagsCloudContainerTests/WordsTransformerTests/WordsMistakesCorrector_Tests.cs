using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Dictionaries;
using TagsCloudContainer.WordsTransformers;

namespace TagsCloudContainerTests.WordsTransformerTests
{
    [TestFixture]
    public class WordsMistakesCorrector_Tests
    {
        private IGrammarDictionary grammarDictionary;
        private Dictionary<string, string> correctForms = new Dictionary<string, string> { {"mistak", "mistake"}, {"incorrect", "correct"} };
        private WordsMistakesCorrector wordsMistakesCorrector;

        [SetUp]
        public void SetUp()
        {
            grammarDictionary = A.Fake<IGrammarDictionary>();
            string answer;
            A.CallTo(() => grammarDictionary.TryGetCorrectForm(A<string>.Ignored, out answer)).Returns(false);
            AddCorrectForms(grammarDictionary, correctForms);
            wordsMistakesCorrector = new WordsMistakesCorrector(grammarDictionary);
        }

        [Test]
        public void TransformWord_ReturnsSameWord_WhenThisWordNotInDictionary()
        {
            var unknownWord = "unknownWord";
            wordsMistakesCorrector.TransformWord(unknownWord).Should().Be(unknownWord);
        }

        [Test]
        public void TransformWord_ReturnsCorrectWord_WhenDictionaryContainsSuchWord()
        {
            var wrongWord = correctForms.First();

            wordsMistakesCorrector.TransformWord(wrongWord.Key).Should().Be(wrongWord.Value);
        }


        private void AddCorrectForms(IGrammarDictionary grammarDictionary, Dictionary<string, string> correctForms)
        {
            string answer;
            foreach (var correctForm in correctForms)
            {
                A.CallTo(() => grammarDictionary.TryGetCorrectForm(correctForm.Key, out answer))
                    .Returns(true)
                    .AssignsOutAndRefParameters(correctForm.Value);
            }
        }
    }
}