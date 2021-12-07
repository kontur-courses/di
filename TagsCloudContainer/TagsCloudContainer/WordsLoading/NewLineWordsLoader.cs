using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.WordsLoading
{
    public class NewLineWordsLoader : IWordsLoader
    {
        public IEnumerable<string> LoadWords(string fileName)
        {
            if (!File.Exists(fileName))
                throw new ApplicationException($"File not exist: {fileName}");

            return File.ReadAllLines(fileName);
        }
    }
}