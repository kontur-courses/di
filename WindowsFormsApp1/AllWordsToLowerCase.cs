using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
    public class AllWordsToLowerCase : ITagFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> tags)
        {
            return tags.Select(x => x.ToLower());
        }
    }
}