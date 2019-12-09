using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Text;

namespace TagsCloudVisualization.Preprocessing
{
    public class InputPreprocessor
    {
        private readonly IPreprocessor[] preprocessors;

        public InputPreprocessor(IPreprocessor[] preprocessors)
        {
            this.preprocessors = preprocessors;
        }

        public Word[] PreprocessWords(ITextReader textReader, string filepath)
        {
            var words = textReader.GetAllWords(filepath);
            return preprocessors
                .Aggregate(words, (current, action) => action.ProcessWords(current))
                .Select(x => new Word(x))
                .ToArray();
        }
    }
}