using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.WordsProvider
{
    public class WordsFromTxtFileProvider : WordsFromFileProvider
    {
        public WordsFromTxtFileProvider(string pathToFile) : base(pathToFile)
        {
        }

        protected override IEnumerable<string> GetText()
        {
            if (!File.Exists(PathToFile))
                throw new Exception($"File {PathToFile} not found");
            return File.ReadLines(PathToFile);
        }
    }
}