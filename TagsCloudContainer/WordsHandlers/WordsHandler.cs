using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.WordsFilters;
using TagsCloudContainer.WordsTransformers;

namespace TagsCloudContainer.WordsHandlers
{
    public class WordsHandler : IWordsHandler
    {
        private readonly IFilter<string>[] filters;
        private readonly IWordsTransformer[] wordsTransformers;

        public WordsHandler(IEnumerable<IFilter<string>> wordsFilters, IEnumerable<IWordsTransformer> wordsTransformers)
        {
            filters = wordsFilters.ToArray();
            this.wordsTransformers = wordsTransformers.ToArray();
        }

        public IEnumerable<WordInfo> HandleWords(IEnumerable<string> words)
        {
            return words.Select(TransformWord)
                .Where(IsCorrectWord)
                .GroupBy(x => x)
                .Select(x => new WordInfo(x.Key, x.Count()));
        }

        private string TransformWord(string word)
        {
            var transformedWord = word;
            foreach (var wordsTransformer in wordsTransformers)
                transformedWord = wordsTransformer.TransformWord(transformedWord);

            return transformedWord;
        }

        private bool IsCorrectWord(string word)
            => filters.All(wordsFilter => wordsFilter.IsCorrect(word));
        
    }
}