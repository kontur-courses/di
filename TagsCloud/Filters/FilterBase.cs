using TagsCloud.Entities;

namespace TagsCloud.Filters;

public abstract class FilterBase
{
    protected readonly FilterOptions options;

    protected FilterBase(FilterOptions options)
    {
        this.options = options;
    }

    public abstract void Apply(List<string> words);
}