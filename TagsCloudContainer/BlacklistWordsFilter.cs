using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class BlacklistWordsFilter : IWordsFilter
    {
        private readonly HashSet<string> blacklist;

        public BlacklistWordsFilter(HashSet<string> blacklist)
        {
            this.blacklist = blacklist;
        }

        public BlacklistWordsFilter(string blacklistPath)
        {
            this.blacklist = new HashSet<string>(new TxtWordsReader().ReadWords(blacklistPath));
        }


        public bool Filter(string word)
        {
            return !blacklist.Contains(word);
        }

        public string TransformWord(string word)
        {
            return word.ToLower();
        }
    }
}