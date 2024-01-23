using TagsCloud.Contracts;
using TagsCloud.Entities;

namespace TagsCloud.Filters;

public class ExcludedFilter : FilterBase, IWordFilter
{
    public ExcludedFilter(FilterOptions options) : base(options)
    {
    }

    public List<string> GetFilteredResult(List<string> words)
    {
        if (Options.ExcludedWords.Count == 0)
            return words;
        
        return words
            .Where(word => !Options.ExcludedWords.Contains(word))
            .ToList();
    }
}