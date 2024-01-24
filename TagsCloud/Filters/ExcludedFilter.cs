using TagsCloud.Entities;

namespace TagsCloud.Filters;

public class ExcludedFilter : FilterBase
{
    public ExcludedFilter(FilterOptions options) : base(options)
    {
    }

    public override void Apply(List<string> words)
    {
        if (Options.ExcludedWords.Count == 0)
            return;

        for (var i = 0; i < words.Count;)
        {
            if (Options.ExcludedWords.Contains(words[i]))
            {
                words.RemoveAt(i);
                continue;
            }

            i++;
        }
    }
}