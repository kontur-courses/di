using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Preprocessing
{
    public class InputPreprocessor
    {
        private readonly IPreprocessor[] preprocessors;

        public InputPreprocessor(IPreprocessor[] preprocessors)
        {
            this.preprocessors = preprocessors;
        }

        public Word[] PreprocessWords(IEnumerable<string> words)
        {
            return preprocessors
                .Aggregate(words, (current, action) => action.ProcessWords(current))
                .Select(x => new Word(x))
                .ToArray();
        }
    }
}