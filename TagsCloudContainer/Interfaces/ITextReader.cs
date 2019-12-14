using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface ITextReader
    {
        IEnumerable<string> Read(string path);
    }
}