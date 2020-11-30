using System.Collections.Generic;

namespace TagsCloud.TextProcessing.TextFilters
{
    public interface IFiltersFactory
    {
        public IEnumerable<string> ApplyFilters(IEnumerable<string> text, string[] filterNames);
        public IEnumerable<string> GetFilerNames();
    }
}
