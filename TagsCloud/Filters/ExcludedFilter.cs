using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Filters;

public class ExcludedFilter : FilterBase
{
    public ExcludedFilter(IFilterOptions options) : base(options)
    {
    }

    public override void Apply(HashSet<WordTagGroup> wordGroups)
    {
        if (options.ExcludedWords.Count == 0)
            return;

        var excluded = options.LanguageParts;
        wordGroups.RemoveWhere(group => excluded.Contains(group.WordInfo.Text));
    }
}