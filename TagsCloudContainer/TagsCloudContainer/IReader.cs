using System.Collections.Generic;


namespace TagsCloudContainer
{
    internal interface IReader
    {
        IEnumerable<string> ReadWords();
    }
}
