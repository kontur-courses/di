using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.WordsLoading
{
    public interface IWordsLoader
    {
        IEnumerable<string> LoadWords(string fileName);
    }

    public class NewLineWordsLoader : IWordsLoader
    {
        public IEnumerable<string> LoadWords(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new ApplicationException($"File not exist: {fileName}");

            return File.ReadAllLines(fileName);
        }
    }
}