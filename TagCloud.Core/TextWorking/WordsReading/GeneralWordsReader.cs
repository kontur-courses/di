using System;
using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles;

namespace TagCloud.Core.TextWorking.WordsReading
{
    public class GeneralWordsReader
    {
        private readonly IWordsReaderForFile[] wordsReaders;

        public GeneralWordsReader(IWordsReaderForFile[] wordsReaders)
        {
            this.wordsReaders = wordsReaders;
        }

        public IEnumerable<string> ReadFrom(string path)
        {
            var suitedReader = wordsReaders.FirstOrDefault(
                r => path.EndsWith(r.ReadingFileExtension, StringComparison.OrdinalIgnoreCase));
            if (suitedReader is null)
                throw new ArgumentException($"Can't find WordsReader for file \"{path}\". Wrong extension");
            return suitedReader.ReadFrom(path);
        }
    }
}