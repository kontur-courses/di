using System.Collections.Generic;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.WordFilter
{
    public class Filters
    {
        private readonly Dictionary<string, IFilter> filtersDictionary;

        public Filters(FilterSettings filterSettings)
        {
            filtersDictionary = new Dictionary<string, IFilter>
            {
                {"length", new LengthWordFilter(filterSettings)},
                {"boring", new BoringWordFilter(filterSettings)}
            };
        }

        public IFilter[] GetFiltersByName(IEnumerable<string> filters)
        {
            var resultFilters = new List<IFilter>();
            foreach (var filter in filters) resultFilters.Add(filtersDictionary[filter]);
            return resultFilters.ToArray();
        }
    }
}