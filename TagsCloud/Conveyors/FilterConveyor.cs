using System.Reflection;
using TagsCloud.CustomAttributes;
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
        foreach (var filter in filters.Order(new FilterComparer()))
            filter.Apply(lines);
        
        lines.TrimExcess();
    }

    private class FilterComparer : IComparer<FilterBase>
    {
        public int Compare(FilterBase? filter, FilterBase? another)
        {
            if (filter == null || another == null)
                throw new ArgumentException("Can't compare null filters!");

            var filterOrder = GetFilterPriority(filter);
            var anotherOrder = GetFilterPriority(another);

            return filterOrder - anotherOrder;
        }

        private static int GetFilterPriority(FilterBase filter)
        {
            var attribute = filter.GetType().GetCustomAttribute(typeof(FilterOrderAttribute));
            return (attribute as FilterOrderAttribute)!.Order;
        }
    }
}