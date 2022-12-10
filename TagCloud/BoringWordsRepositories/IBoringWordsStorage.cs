using System.Collections.Generic;

namespace TagCloud.BoringWordsRepositories
{
    public interface IBoringWordsStorage
    {
        public string FileExtFilter { get; }
        public HashSet<string> GetBoringWords();
        public HashSet<string> GetBoringWords(string path);
    }
}
