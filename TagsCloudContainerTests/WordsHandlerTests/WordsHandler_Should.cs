using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using TagsCloudContainer.WordsFilters;
using TagsCloudContainer.WordsHandlers;
using TagsCloudContainer.WordsTransformers;
using FluentAssertions;
using FluentAssertions.Common;

namespace TagsCloudContainerTests.WordsHandlerTests
{
    [TestFixture]
    public class WordsHandler_Should
    {
        private IFilter<string> wordsFilter;
        private IWordsTransformer wordsTransformer;
        private WordsHandler wordsHandler;
        private readonly List<string> words = new List<string> {"ab", "acd", "eded", "wwww", "ab"};

        [SetUp]
        public void SetUp()
        {
            wordsFilter = new Fake<IFilter<string>>().FakedObject;
            A.CallTo(() => wordsFilter.IsCorrect(A<string>.Ignored)).Returns(true);
            wordsTransformer = GetBaseWordsTransformer(words);
            wordsHandler = new WordsHandler(new [] {wordsFilter}, new [] {wordsTransformer} );
        }

        [Test]
        public void HandleWords_ContainsGivenWords_InTotal_WhenThereAreNoExcludedWords()
        {
            var actualWords = wordsHandler.HandleWords(words).Select(x => x.Word);

            actualWords.Should().Contain(words);
        }

        [Test]
        public void HandleWords_RemovesRepeatingWords()
        {
            var wordsInfos = wordsHandler.HandleWords(words);

            wordsInfos.Select(x => x.Word).Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void HandleWords_ContainsWordInfos_WithCorrectRepetitions()
        {
            var wordInfos = wordsHandler.HandleWords(words);

            wordInfos.Should().OnlyContain(x => x.Repetitions == words.FindAll(y => y.IsSameOrEqualTo(x.Word)).Count);
        }

        [Test]
        public void HandleWords_RemovesWords_WhichDoNotSatisfyFilter()
        {
            A.CallTo(() => wordsFilter.IsCorrect("ab")).Returns(false);

            var wordsInfos = wordsHandler.HandleWords(words);

            wordsInfos.Should().NotContain(x => x.Word == "ab");
        }

        [Test]
        public void HandleWords_RemovesBadWords_WhenThereAreMultipleFilters()
        {
            var firstFilter = GetFilterWithExcludedWord("ab");
            var secondFilter = GetFilterWithExcludedWord("acd");
            var wordsHandler = new WordsHandler(new [] {firstFilter, secondFilter}, new IWordsTransformer[] {});

            var wordsInfos = wordsHandler.HandleWords(words);

            wordsInfos.Should().NotContain(x => x.Word == "ab" || x.Word == "acd");
        }

        [Test]
        public void HandleWords_TransformsWords_WhenThereAreTransformers()
        {
            var changedWord = words.First();
            var newWord = "new_word";
            A.CallTo(() => wordsTransformer.TransformWord(changedWord)).Returns(newWord);

            var wordsInfos = wordsHandler.HandleWords(words);

            wordsInfos.Should().Contain(x => x.Word == newWord).And.NotContain(x => x.Word == changedWord);
        }

        [Test]
        public void HandleWords_TransformsWords_WorksInCorrectOrder_WhenThereAreMultipleTransformers()
        {
            var firstWord = words.First();
            var intermediateForm = "form";
            var lastForm = "new";
            var firstTransformer = GetBaseTransformerWithOneChangedWords(words, firstWord, intermediateForm);
            var secondTransformer = GetBaseTransformerWithOneChangedWords(words, intermediateForm, lastForm);
            var wordsHandler = new WordsHandler(new IFilter<string>[] {}, new [] {firstTransformer, secondTransformer});

            var wordInfos = wordsHandler.HandleWords(words);

            wordInfos.Should().Contain(x => x.Word == lastForm).And
                .NotContain(x => x.Word == firstWord || x.Word == intermediateForm);
        }

        [Test]
        public void HandleWords_TransformsWords_ThenFiltersThem()
        {
            var word = words.First();
            var transformedWord = word + word;
            A.CallTo(() => wordsTransformer.TransformWord(word)).Returns(transformedWord);
            A.CallTo(() => wordsFilter.IsCorrect(transformedWord)).Returns(false);

            var wordInfos = wordsHandler.HandleWords(words);

            wordInfos.Should().NotContain(x => x.Word == transformedWord);
        }
        

        private IFilter<string> GetFilterWithExcludedWord(string word)
        {
            var filter = new Fake<IFilter<string>>().FakedObject;
            A.CallTo(() => filter.IsCorrect(A<string>.Ignored)).Returns(true);
            A.CallTo(() => filter.IsCorrect(word)).Returns(false);
            return filter;
        }

        private IWordsTransformer GetBaseWordsTransformer(IEnumerable<string> words)
        {
            var wordsTransformer = new Fake<IWordsTransformer>().FakedObject;
            A.CallTo(() => wordsTransformer.TransformWord(A<string>.Ignored)).Returns("word");
            foreach (var word in words)
            {
                A.CallTo(() => wordsTransformer.TransformWord(word)).Returns(word);
            }

            return wordsTransformer;
        }

        private IWordsTransformer GetBaseTransformerWithOneChangedWords(IEnumerable<string> words, string previousWord,
            string newWord)
        {
            var wordsTransformer = GetBaseWordsTransformer(words);
            A.CallTo(() => wordsTransformer.TransformWord(previousWord)).Returns(newWord);
            return wordsTransformer;
        }

    }
}