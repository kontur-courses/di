using TagsCloud.Contracts;

namespace TagsCloud.Filters;

public abstract class FilterBase
{
    protected readonly IFilterOptions options;

    protected FilterBase(IFilterOptions options)
    {
        this.options = options;
    }

    public abstract void Apply(List<string> words);
}