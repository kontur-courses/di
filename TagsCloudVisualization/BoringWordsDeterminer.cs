using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class BoringWordsDeterminer : IBoringWordDeterminer
    {
        private readonly HashSet<string> boringWordsSet = new HashSet<string>()
        {
            "and", "the", "a", "in", "for", "of", "about"
        };
        public bool IsBoringWord(string word)
        {
            return boringWordsSet.Contains(word);
        }
    }
}