using System.Collections.Generic;

namespace TagsCloudContainer.Readers
{
    public interface IReader
    {
        IEnumerable<string> Read(string fileName);
    }
}