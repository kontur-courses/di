using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextFilter
{
    public class TextFilter
    {
        private readonly BlacklistMaker blacklistMaker;
        private readonly TextFilterSettings textFilterSettings;
        private Dictionary<string, int> filteredWords;

        public TextFilter(BlacklistMaker blacklistMaker, TextFilterSettings textFilterSettings)
        {
            this.blacklistMaker = blacklistMaker;
            this.textFilterSettings = textFilterSettings;
        }

        public Dictionary<string, int> FilterWords(Dictionary<string, int> words)
        {
            filteredWords = new Dictionary<string, int>();
            foreach (var word in words.Where(word =>
                !blacklistMaker.BlackList.Contains(word.Key)
                && word.Key.Length >= textFilterSettings.WordMinLength))
                filteredWords[word.Key] = word.Value;
            return filteredWords;
        }
    }
}