using TagsCloud.Filters;

namespace TagsCloud.Conveyors;

public class FilterConveyor
{
    private readonly IEnumerable<FilterBase> filters;

    public FilterConveyor(IEnumerable<FilterBase> filters)
    {
        this.filters = filters;
    }

    public void ApplyFilters(List<string> lines)
    {
        foreach (var filter in filters)
            filter.Apply(lines);
    }
}