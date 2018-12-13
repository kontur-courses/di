using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud.Core.WordsParsing.WordsReading
{
    public class GeneralWordsReader : IWordsReader
    {
        private readonly IWordsReader[] wordsReaders;

        public GeneralWordsReader(IWordsReader[] wordsReaders)
        {
            this.wordsReaders = wordsReaders;
            AllowedFileExtension = new Regex(@".*$");
        }

        public IEnumerable<string> ReadFrom(string path)
        {
            var suitedReader = wordsReaders.FirstOrDefault(r => r.AllowedFileExtension.Match(path).Success);
            if (suitedReader is null)
                throw new ArgumentException($"Can't find WordsReader for file \"{path}\". Wrong extension");
            return suitedReader.ReadFrom(path);
        }

        public Regex AllowedFileExtension { get; }
    }
}