using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloudVisualization;

namespace TagsCloud.Filters;

[Injection(ServiceLifetime.Singleton)]
public class SpeechPartFilter : IFilter
{
    public void Apply(HashSet<WordTagGroup> wordGroups, IFilterOptions options)
    {
        var parts = options.LanguageParts;
        wordGroups.RemoveWhere(group =>
        {
            var wordInfo = group.WordInfo;

            if (!parts.Contains(wordInfo.LanguagePart))
                return options.OnlyRussian || wordInfo.IsRussian;

            return false;
        });
    }
}