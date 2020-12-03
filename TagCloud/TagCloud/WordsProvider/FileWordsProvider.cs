using System;
using System.Collections.Generic;

namespace TagCloud.WordsProvider
{
    public abstract class FileWordsProvider : IWordsProvider
    {
        protected readonly string FilePath;

        protected FileWordsProvider(string filePath)
        {
            if (!CheckFile(filePath))
                throw new ArgumentException("File is incorrect");
            FilePath = filePath;
        }

        public abstract IEnumerable<string> GetWords();

        protected abstract bool CheckFile(string filePath);
    }
}