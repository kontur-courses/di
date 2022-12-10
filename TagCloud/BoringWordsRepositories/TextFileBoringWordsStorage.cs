using System.Collections.Generic;
using System.Linq;
using TagCloud.Readers;

namespace TagCloud.BoringWordsRepositories
{
    public class TextFileBoringWordsStorage : SingleWordInRowTextFileReader, IBoringWordsStorage
    {
        public HashSet<string> GetBoringWords() =>
            GetBoringWords(@"BoringWordsRepositories\BoringWordsDictionary.txt");

        public HashSet<string> GetBoringWords(string path)
        {
            Open(path);
            return ReadWords()
                .Select(word => word.ToLower())
                .ToHashSet();
        }
    }
}
