using System.Collections.Generic;
using System.Linq;
using NSubstitute;

namespace TagsCloudContainer
{
    class BoringWordsFormater : IWordFormater
    {
        public string[] BoringWords { get; set; }

        public BoringWordsFormater(string[] boringWords)
        {
            BoringWords = boringWords;
        }

        public IEnumerable<string> HandleWords(IEnumerable<string> words)
        {
            return words.Where(w => !BoringWords.Contains(w)).ToArray();
        }
    }
}