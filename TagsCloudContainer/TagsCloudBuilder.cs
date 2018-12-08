﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordFormatters;
using TagsCloudContainer.WordLayouts;
using TagsCloudContainer.WordsPreprocessors;

namespace TagsCloudContainer
{
    public class TagsCloudBuilder
    {
        private readonly IWordsPreprocessor[] wordsPreprocessors;
        private readonly IWordFormatter wordFormatter;
        private readonly ILayouter layouter;
        private readonly IResultRenderer resultRenderer;

        public TagsCloudBuilder(
            IWordsPreprocessor[] wordsPreprocessors,
            IWordFormatter wordFormatter,
            ILayouter layouter,
            IResultRenderer resultRenderer)
        {
            this.wordsPreprocessors = wordsPreprocessors;
            this.wordFormatter = wordFormatter;
            this.layouter = layouter;
            this.resultRenderer = resultRenderer;
        }

        public Image Visualize(IEnumerable<string> rawWords)
        {
            rawWords = wordsPreprocessors.Aggregate(rawWords,
                (current, preprocessor) => preprocessor.Preprocess(current));
            var words = wordFormatter.FormatWords(rawWords);
            var positionedWords = words
                .Select(word => new Word(word.Font, word.Color, word.Value)
                {
                    Position = layouter
                        .GetNextPosition(resultRenderer.GetWordSize(word))
                });

            return resultRenderer.Generate(positionedWords);
        }
    }
}