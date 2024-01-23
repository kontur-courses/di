using TagsCloud.Contracts;

namespace TagsCloud;

public class FilterConveyor
{
    private readonly IEnumerable<IWordFilter> filters;

    public FilterConveyor(IEnumerable<IWordFilter> filters)
    {
        this.filters = filters;
    }

    public List<string> ApplyFilters(List<string> lines)
    {
        return filters.Aggregate(lines, (current, filter) => filter.GetFilteredResult(current));
    }
}