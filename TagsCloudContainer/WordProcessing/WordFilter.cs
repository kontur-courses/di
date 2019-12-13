using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordProcessing
{
    class WordFilter : IWordFilter
    {
        private readonly Func<string, bool> filterFunc;
        
        public WordFilter(Func<string, bool> filterFunc)
        {
            this.filterFunc = filterFunc;
        }

        public WordFilter() : this(x => true)
        {
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(word => filterFunc(word));
        }
    }
}