using System.Reflection;
using TagsCloud.CustomAttributes;
using TagsCloud.Entities;
using TagsCloud.Filters;

namespace TagsCloud.Conveyors;

public sealed class FilterConveyor
{
    private readonly IEnumerable<FilterBase> filters;

    public FilterConveyor(IEnumerable<FilterBase> filters)
    {
        this.filters = filters.Order(new FilterComparer());
    }

    public void ApplyFilters(List<WordToStatus> words)
    {
        foreach (var filter in filters)
            filter.Apply(words);
    }

    private class FilterComparer : IComparer<FilterBase>
    {
        public int Compare(FilterBase filter, FilterBase another)
        {
            if (filter == null || another == null)
                throw new ArgumentException("Can't compare null filters!");

            var filterOrder = GetFilterPriority(filter);
            var anotherOrder = GetFilterPriority(another);

            return filterOrder - anotherOrder;
        }

        private static int GetFilterPriority(FilterBase filter)
        {
            var attribute = filter.GetType().GetCustomAttribute<FilterOrderAttribute>();
            return attribute!.Order;
        }
    }
}