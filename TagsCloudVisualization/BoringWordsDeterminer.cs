using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public class BoringWordsDeterminer : IBoringWordDeterminer
    {
        private readonly HashSet<string> boringWordsSet = new HashSet<string>()
        {
            "and", "the", "a", "in", "for", "of", "about", "to",
            "и", "не","на", "из","в","с","о","за","от","что",
        };
        public bool IsBoringWord(string word)
        {
            return boringWordsSet.Contains(word);
        }
    }
}