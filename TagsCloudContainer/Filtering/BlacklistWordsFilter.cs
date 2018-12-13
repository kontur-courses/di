using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Filtering
{
    public class BlacklistWordsFilter : IWordsFilter
    {
        public HashSet<string> Blacklist { get; set; }

       

        public BlacklistWordsFilter(HashSet<string> blacklist)
        {
            Blacklist = blacklist;
        }


        public List<string> Filter(IEnumerable<string> words)
        {
            return words.Where(x => !Blacklist.Contains(x)).ToList();
        }
    }
}