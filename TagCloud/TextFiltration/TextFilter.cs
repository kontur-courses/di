using System.Collections.Generic;
using System.Linq;
using TagCloud.TextFilterConditions;
using TagCloud.TextParser;

namespace TagCloud.TextFiltration
{
    public class TextFilter
    {
        private readonly IEnumerable<IFilterCondition> filterConditions;
        private readonly ITextParser textParser;

        public TextFilter(ITextParser textParser,
            IEnumerable<IFilterCondition> filterConditions)
        {
            this.filterConditions = filterConditions;
            this.textParser = textParser;
        }

        public List<string> FilterWords()
        {
            var allWords = textParser.ParseText();
            return allWords.Where(PassThroughAllFilters).ToList();
        }

        private bool PassThroughAllFilters(string s)
        {
            return filterConditions.All(condition => condition.CheckFilterCondition(s))
                   || !filterConditions.Any();
        }
    }
}