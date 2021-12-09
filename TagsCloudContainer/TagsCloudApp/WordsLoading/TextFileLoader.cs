using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudApp.WordsLoading
{
    public abstract class TextFileLoader : IFileTextLoader
    {
        public abstract IEnumerable<FileType> SupportedTypes { get; }

        public string LoadText(string filename)
        {
            if (!File.Exists(filename))
                throw new ApplicationException($"File not exist: {filename}");

            return LoadTextFromExistingFile(filename);
        }

        protected abstract string LoadTextFromExistingFile(string filename);
    }
}