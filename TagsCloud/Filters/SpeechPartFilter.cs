using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Filters;

public class SpeechPartFilter : IFilter
{
    public void Apply(HashSet<WordTagGroup> wordGroups, IFilterOptions options)
    {
        var parts = options.LanguageParts;
        wordGroups.RemoveWhere(group => !parts.Contains(group.WordInfo.LanguagePart));
    }
}