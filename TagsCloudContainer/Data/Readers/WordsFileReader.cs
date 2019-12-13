using System.Collections.Generic;

namespace TagsCloudContainer.Data.Readers
{
    public class WordsFileReader : IWordsFileReader
    {
        private readonly IDictionary<string, IWordsFileReader> readers;

        public WordsFileReader(IEnumerable<IFileFormatReader> readers)
        {
            this.readers = new Dictionary<string, IWordsFileReader>();
            foreach (var reader in readers)
            foreach (var extension in reader.Extensions)
                this.readers[extension] = reader;
        }

        public IEnumerable<string> ReadAllWords(string path)
        {
            return readers[PathUtils.GetExtension(path)].ReadAllWords(path);
        }
    }
}