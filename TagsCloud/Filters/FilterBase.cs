using TagsCloud.Entities;

namespace TagsCloud.Filters;

public abstract class FilterBase
{
    protected readonly FilterOptions Options;

    protected FilterBase(FilterOptions options)
    {
        Options = options;
    }
}