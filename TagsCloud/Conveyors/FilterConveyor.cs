using TagsCloud.Filters;
using TagsCloudVisualization;

namespace TagsCloud.Conveyors;

public sealed class FilterConveyor
{
    private readonly IEnumerable<FilterBase> filters;

    public FilterConveyor(IEnumerable<FilterBase> filters)
    {
        this.filters = filters;
    }

    public void ApplyFilters(HashSet<WordTagGroup> wordGroups)
    {
        foreach (var filter in filters)
            filter.Apply(wordGroups);
    }
}