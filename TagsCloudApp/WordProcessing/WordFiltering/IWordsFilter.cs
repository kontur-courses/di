using System.Collections.Generic;

namespace TagsCloudApp.WordFiltering
{
    public interface IWordFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}
