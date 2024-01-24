using TagsCloud.Contracts;
using TagsCloud.CustomAttributes;

namespace TagsCloud.Filters;

[FilterOrder(1)]
public class ExcludedFilter : FilterBase
{
    public ExcludedFilter(IFilterOptions options) : base(options)
    {
    }

    public override void Apply(List<string> words)
    {
        if (options.ExcludedWords.Length == 0)
            return;

        for (var i = 0; i < words.Count;)
        {
            if (options.ExcludedWords.Contains(words[i]))
            {
                words.RemoveAt(i);
                continue;
            }

            i++;
        }
    }
}