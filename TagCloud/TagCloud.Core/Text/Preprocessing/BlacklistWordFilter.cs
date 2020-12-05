using System.Collections.Generic;

namespace TagCloud.Core.Text.Preprocessing
{
    public class BlacklistWordFilter : IWordFilter
    {
        private static readonly HashSet<string> blacklistedWords = new HashSet<string>
        {
            "в", "без", "до", "для", "за", "через", "над", "по", "из", "у", "около", "под", "о", "про", "на", "к",
            "перед", "при", "с", "между", "как", "что", "где", "не", "ни", "вовсе", "вот", "это"
        };

        public bool IsValidWord(string word) => !blacklistedWords.Contains(word);
    }
}