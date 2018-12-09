using System.Collections.Generic;

namespace TagsCloud.Tests
{
    public class ConstWordCollection : IBoringWordsCollection
    {
        private readonly List<string> words;

        public ConstWordCollection(List<string> words)
        {
            this.words = words;
        }

        public IEnumerable<string> DeleteBoringWords()
        {
            return words;
        }
    }
}