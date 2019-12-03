using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
    public interface IWordStream
    {
        IEnumerable<string> GetWords(string path);
    }
}
