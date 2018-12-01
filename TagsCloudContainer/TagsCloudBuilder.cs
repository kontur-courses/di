using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;
using TagsCloudContainer.WordsReaders;

namespace TagsCloudContainer
{
    public class TagsCloudBuilder
    {
        private readonly IWordsReader wordsReader;
        private readonly IWordsPreprocessor[] wordsPreprocessors;
        private readonly IWordFormatter wordFormatter;
        private readonly IWordLayout wordLayout;
        private readonly IResultRenderer resultRenderer;

        public TagsCloudBuilder(IWordsReader wordsReader,
            IWordsPreprocessor[] wordsPreprocessors,
            IWordFormatter wordFormatter,
            IWordLayout wordLayout,
            IResultRenderer resultRenderer)
        {
            this.wordsReader = wordsReader;
            this.wordsPreprocessors = wordsPreprocessors;
            this.wordFormatter = wordFormatter;
            this.wordLayout = wordLayout;
            this.resultRenderer = resultRenderer;
        }

        public void Visualize(string inputFilename, string outputFilename)
        {
            var rawWords = wordsReader.GetWords(inputFilename);
            rawWords = wordsPreprocessors.Aggregate(rawWords,
                (current, preprocessor) => preprocessor.Preprocess(current));
            var words = wordFormatter.FormatWords(rawWords)
                .Select(word => (Word)word);
            var positionedWords = words
                .Select(word => wordLayout.PositionNextWord(word, resultRenderer.GetWordSize(word)) as Word)
                .ToList();
            resultRenderer.Generate(positionedWords, outputFilename);
        }
    }
}