using System.Collections.Generic;
using System.Linq;
using TagCloud.BoringWordsStorage;
using TagCloud.Readers;

namespace TagCloud.BoringWordsRepositories
{
    public class TextFileBoringWordsStorage : SingleWordInRowTextFileReader, IBoringWordsStorage
    {
        public TextFileBoringWordsStorage(string path = "BoringWordsRepositories\\BoringWordsDictionary.txt") : base(path)
        {
        }

        public HashSet<string> GetBoringWords()
        {
            return ReadWords()
                .Select(word => word.ToLower())
                .ToHashSet();
        }
    }
}
