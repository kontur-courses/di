using System.Collections.Generic;

namespace TagsCloud
{
    public interface IWordCollection
    {
        IEnumerable<string> GetWords();
    }
}