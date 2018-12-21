using System.Collections.Generic;

namespace TagsCloudBuilder
{
    public interface IWordsPreparer
    {
        Dictionary<string, int> GetPreparedWords();
    }
}
