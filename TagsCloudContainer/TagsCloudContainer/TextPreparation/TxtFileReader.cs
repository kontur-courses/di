using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.TextPreparation
{
    public class TxtFileReader : IFileReader
    {
        private readonly IWordsReader wordsReader;

        public TxtFileReader(IWordsReader wordsReader)
        {
            this.wordsReader = wordsReader;
        }

        public List<string> GetAllWords(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentException("Path can't be null");
            }

            using var reader = new StreamReader(filePath);
            return wordsReader.ReadAllWords(reader.ReadToEnd());
        }
    }
}