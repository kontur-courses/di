using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsProvider
{
    public abstract class WordsFromFileProvider : IWordsProvider
    {
        protected readonly string PathToFile;

        protected WordsFromFileProvider(string pathToFile)
        {
            PathToFile = pathToFile ?? throw new ArgumentNullException(nameof(pathToFile));
        }

        public IEnumerable<string> GetWords() => GetText().SelectMany(WordSplitter.Split);

        protected abstract IEnumerable<string> GetText();
    }
}