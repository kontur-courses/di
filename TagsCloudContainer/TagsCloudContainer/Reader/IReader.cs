using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Reader
{
    public interface IReader
    {
        IEnumerable<string> GetWorldSet(string path);
    }
}