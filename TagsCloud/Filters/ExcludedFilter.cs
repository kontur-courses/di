using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Filters;

public class ExcludedFilter : IFilter
{
    public void Apply(HashSet<WordTagGroup> wordGroups, IFilterOptions options)
    {
        if (options.ExcludedWords.Count == 0)
            return;

        var excluded = options.ExcludedWords;
        wordGroups.RemoveWhere(group => excluded.Contains(group.WordInfo.Text));
    }
}