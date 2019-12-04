using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsSelector
    {
        IEnumerable<LayoutWord> Select();
    }
}