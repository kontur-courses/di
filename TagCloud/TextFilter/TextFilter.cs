using System.Collections.Generic;
using System.Linq;
using TagCloud.TextProvider;
using TagCloudForm.TextFilterConditions;

namespace TagCloud.TextFilter
{
    public class TextFilter
    {
        private readonly BlacklistMaker blacklistMaker;
        private readonly IEnumerable<IFilterCondition> filterConditions;
        private readonly ITextParser textParser;

        public TextFilter(ITextParser textParser, BlacklistMaker blacklistMaker,
            IEnumerable<IFilterCondition> filterConditions)
        {
            this.blacklistMaker = blacklistMaker;
            this.filterConditions = filterConditions;
            this.textParser = textParser;
        }

        public List<string> FilterWords()
        {
            var allWords = textParser.ParseText();
//            return allWords.Where(word => word.Length >= blacklistMaker.WordMinLength
//                                          && !blacklistMaker.BlackList.Contains(word)).ToList();
            return allWords.Where(PassThroughAllFilters).ToList();
        }

        private bool PassThroughAllFilters(string s)
        {
            return filterConditions.All(condition => condition.CheckFilterCondition(s))
                   || filterConditions.Count() == 0;
        }
    }
}