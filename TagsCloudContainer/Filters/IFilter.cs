using System.Collections.Generic;

namespace TagsCloudContainer.Filters
{
    public interface IFilter
    {
        IEnumerable<string> Filtering(IEnumerable<string> tokens);
    }
}
