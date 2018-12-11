using System.Collections.Generic;

namespace TagsCloudContainer.WordFilter
{
    public class Filters
    {
        public static readonly Dictionary<string, IFilter> FiltersDictionary = new Dictionary<string, IFilter>
        {
            {"length", new LengthWordFilter(4)},
            {"simple", new NotNullWordFilter()}
        };
    }
}