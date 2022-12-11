using System.Collections.Generic;
using System.Linq;
using TagCloud.Readers;

namespace TagCloud.BoringWordsRepositories
{
    public class TextFileBoringWordsStorage : IBoringWordsStorage
    {
        private readonly IBoringWordsReader reader;
        private HashSet<string> boringWords;

        public TextFileBoringWordsStorage(IBoringWordsReader reader)
        {
            boringWords = new HashSet<string>();
            this.reader = reader;
        }

        public string FileExtFilter => reader.FileExtFilter;

        public void LoadBoringWords(string path)
        {
            boringWords.Clear();
            reader.SetFile(path);
            boringWords = reader.ReadWords().
                Select(word => word.ToLower())
                .ToHashSet();
        }

        public HashSet<string> GetBoringWords() => boringWords;
    }
}
