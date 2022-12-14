using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer;

namespace TagsCloudGUI
{
    public class WordFilter : IWordFilter
    {
        public HashSet<string> WordsToFilter { get; set; }

        public WordFilter()
        {
            WordsToFilter = new HashSet<string>();
        }
    }
}