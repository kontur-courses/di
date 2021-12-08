using System;
using System.IO;

namespace TagsCloudContainer.WordsLoading
{
    public abstract class TextFileLoader : IFileTextLoader
    {
        public string LoadText(string filename)
        {
            if (!File.Exists(filename))
                throw new ApplicationException($"File not exist: {filename}");

            return LoadTextFromExistingFile(filename);
        }

        protected abstract string LoadTextFromExistingFile(string filename);
    }
}