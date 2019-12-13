using System.Collections.Generic;
using TagsCloud.WordsFiltering;

namespace TagsCloud
{
    public class WordsFilterer
    {
        private readonly IFilter[] filters;

        public WordsFilterer(IFilter[] filters)
        {
            this.filters = filters;
        }

        public List<string> FilterWords(List<string> words)
        {
            var res = new List<string>(words);
            foreach (var filter in filters)
                res = filter.FilterFunc(res);
            return res;
        }
    }
}
