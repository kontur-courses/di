using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Readers;

namespace TagCloud.BoringWordsRepositories
{
    public class TextFileBoringWordsStorage : SingleWordInRowTextFileReader, IBoringWordsStorage
    {
        private HashSet<string> boringWords;

        public TextFileBoringWordsStorage()
        {
            boringWords = new HashSet<string>();
        }

        public void LoadBoringWords(string path)
        {
            boringWords.Clear();
            Open(path);
            boringWords = ReadWords().
                Select(word => word.ToLower())
                .ToHashSet();
        }

        public HashSet<string> GetBoringWords() => boringWords;
    }
}
