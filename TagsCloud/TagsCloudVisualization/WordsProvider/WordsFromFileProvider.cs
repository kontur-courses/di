using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProvider
{
    public class WordsFromFileProvider : IWordsProvider
    {
        private readonly string _pathToFile;

        public WordsFromFileProvider(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public IEnumerable<string> GetWords() => throw new NotImplementedException();
    }
}