using System.Collections.Generic;

namespace TagsCloudContainer.TextParsers
{
    public interface IExcludingWords
    {
        HashSet<string> GetWords();
    }
}
