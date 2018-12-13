using System.Collections.Generic;

namespace TagsCloudContainer.Filter
{
    public interface IFilter
    {
        IEnumerable<string> FilterOut(IEnumerable<string> words);
    }
}