using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Conveyors;

public sealed class FilterConveyor
{
    private readonly IEnumerable<IFilter> filters;
    private readonly IFilterOptions filterOptions;

    public FilterConveyor(IEnumerable<IFilter> filters, IFilterOptions filterOptions)
    {
        this.filters = filters;
        this.filterOptions = filterOptions;
    }

    public void ApplyFilters(HashSet<WordTagGroup> wordGroups)
    {
        foreach (var filter in filters)
            filter.Apply(wordGroups, filterOptions);
    }
}