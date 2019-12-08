using System.Collections.Generic;
using System.Linq;
using TagCloud.TextProvider;

namespace TagCloud.TextFilter
{
    public class TextFilter
    {
        private readonly BlacklistMaker blacklistMaker;
        private readonly ITextProvider textProvider;

        public TextFilter(ITextProvider textProvider, BlacklistMaker blacklistMaker)
        {
            this.blacklistMaker = blacklistMaker;
            this.textProvider = textProvider;
        }

        public List<string> FilterWords()
        {
            var allWords = textProvider.GetAllWords();
            return allWords.Where(word => word.Length >= blacklistMaker.WordMinLength
                                          && !blacklistMaker.BlackList.Contains(word)).ToList();
        }
    }
}