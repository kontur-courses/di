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
        private readonly ILayouter layouter;
        private readonly IResultRenderer resultRenderer;

        public TagsCloudBuilder(IWordsReader wordsReader,
            IWordsPreprocessor[] wordsPreprocessors,
            IWordFormatter wordFormatter,
            ILayouter layouter,
            IResultRenderer resultRenderer)
        {
            this.wordsReader = wordsReader;
            this.wordsPreprocessors = wordsPreprocessors;
            this.wordFormatter = wordFormatter;
            this.layouter = layouter;
            this.resultRenderer = resultRenderer;
        }

        public void Visualize(string inputFilename, string outputFilename)
        {
            var rawWords = wordsReader.GetWords(inputFilename);
            rawWords = wordsPreprocessors.Aggregate(rawWords,
                (current, preprocessor) => preprocessor.Preprocess(current));
            var words = wordFormatter.FormatWords(rawWords)
                .Select(word => word);
            var positionedWords = words
                .Select(word =>
                {
                    word.Position = layouter
                        .GetNextPosition(word, resultRenderer.GetWordSize(word))
                        .Position;

                    return word;
                })
                .ToList();
            resultRenderer.Generate(positionedWords, outputFilename);
        }
    }
}