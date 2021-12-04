using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.WordsProvider
{
    public class WordsFromFileProvider : IWordsProvider
    {
        private readonly string _pathToFile;

        public WordsFromFileProvider(string pathToFile)
        {
            _pathToFile = pathToFile ?? throw new ArgumentNullException(nameof(pathToFile));
        }

        public IEnumerable<string> GetWords()
        {
            if (!File.Exists(_pathToFile))
                throw new Exception($"File {_pathToFile} not found");
            return File.ReadLines(_pathToFile);
        }
    }
}