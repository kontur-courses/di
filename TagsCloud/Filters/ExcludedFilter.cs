using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;

namespace TagsCloud.Filters;

[FilterOrder(1)]
public class ExcludedFilter : FilterBase
{
    public ExcludedFilter(IFilterOptions options) : base(options)
    {
    }

    public override void Apply(List<WordToStatus> words)
    {
        if (options.ExcludedWords.Count == 0)
            return;

        foreach (var word in words.Where(word => options.ExcludedWords.Contains(word.Word)))
            word.IsTrash = true;
    }
}