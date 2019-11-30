using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Preprocessing
{
    public class Preprocessor
    {
        private readonly IPreprocessAction[] actions;

        public Preprocessor(IPreprocessAction[] preprocessActions)
        {
            actions = preprocessActions;
        }

        public Word[] PreprocessWords(IEnumerable<Word> words)
        {
            return actions
                .Aggregate(words, (current, action) => action.ProcessWords(current))
                .ToArray();
        }
    }
}