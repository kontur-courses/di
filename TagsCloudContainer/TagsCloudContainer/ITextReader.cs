using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ITextReader
    {
        IEnumerable<string> GetLines();
    }

}