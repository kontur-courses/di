using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Filters;

public class SpeechPartFilter : FilterBase
{
    public SpeechPartFilter(IFilterOptions options) : base(options)
    {
    }

    public override void Apply(HashSet<WordTagGroup> wordGroups)
    {
        var parts = options.LanguageParts;
        wordGroups.RemoveWhere(group => !parts.Contains(group.WordInfo.LanguagePart));
    }
}