using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.WordsLoading
{
    public interface IWordsProvider
    {
        IEnumerable<string> GetWords();
    }

    public class NewLineWordLoader : IWordsProvider
    {
        private readonly string filePath;

        public NewLineWordLoader(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new ApplicationException($"File not exist: {filePath}");

            this.filePath = filePath;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(filePath);
        }
    }
}