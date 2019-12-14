using System.Collections;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ITextReader
    {
        IEnumerable<string> Read(string path);
    }
}