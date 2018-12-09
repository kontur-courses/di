using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Algorithms;
using TagsCloudContainer.SourceTextReaders;
using TagsCloudContainer.TextPreprocessors;

namespace TagsCloudContainer.DataProviders
{
    public class DataProvider : IDataProvider
    {
        private readonly ISourceTextReader textReader;

        private readonly IWordsPreprocessor wordsPreprocessor;

        private readonly IAlgorithm algorithm;

        public DataProvider(ISourceTextReader textReader, IWordsPreprocessor wordsPreprocessor, IAlgorithm algorithm)
        {
            this.textReader = textReader;
            this.wordsPreprocessor = wordsPreprocessor;
            this.algorithm = algorithm;
        }

        public IReadOnlyDictionary<string, (Rectangle, int)> GetData()
        {
            var lines = textReader.ReadText();
            var preprocessedWords = wordsPreprocessor.PreprocessWords(lines);

            return algorithm.GenerateRectanglesSet(preprocessedWords
                .OrderByDescending(e => e.Value)
                .Take(100).ToDictionary(e => e.Key, e => e.Value));
        }
    }
}