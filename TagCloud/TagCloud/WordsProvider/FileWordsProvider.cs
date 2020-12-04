using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.WordsProvider
{
    public abstract class FileWordsProvider : IWordsProvider
    {
        protected readonly string FilePath;
        public abstract string[] SupportedExtensions { get; }

        protected FileWordsProvider(string filePath)
        {
            FilePath = filePath;
        }

        public abstract IEnumerable<string> GetWords();

        protected bool CheckFile(string filePath)
        {
            return SupportedExtensions.Any(extension => filePath.EndsWith(extension));
        }
    }
}