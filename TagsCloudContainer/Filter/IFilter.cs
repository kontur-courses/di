using System.Collections.Generic;

namespace TagsCloudContainer.Filter
{
    public interface IFilter
    {
        IEnumerable<string> Filtrate(IEnumerable<string> words);
    }
}