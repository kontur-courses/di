using TagsCloud.CustomAttributes;
using TagsCloud.Entities;

namespace TagsCloud.Filters;

[FilterOrder(1)]
public class ExcludedFilter : FilterBase
{
    public ExcludedFilter(FilterOptions options) : base(options)
    {
    }

    public override void Apply(List<string> words)
    {
        if (options.ExcludedWords.Count == 0)
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