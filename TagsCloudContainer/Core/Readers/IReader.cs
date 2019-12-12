using System.Collections.Generic;

namespace TagsCloudContainer.Core.Readers
{
    interface IReader
    {
        IEnumerable<string> ReadWords(string path);
        bool CanRead(string path);
    }
}