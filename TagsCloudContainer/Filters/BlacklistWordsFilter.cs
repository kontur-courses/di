using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Filters
{
    public class BlacklistWordsFilter : IWordsFilter
    {
        public HashSet<string> Blacklist { get; set; }

        public BlacklistWordsFilter(BlackListFilterSettings settings)
        {
            Blacklist = settings.Blacklist;
        }


        public List<string> Filter(List<string> words)
        {
            return words.Where(x => !Blacklist.Contains(x)).ToList();
        }
    }
}