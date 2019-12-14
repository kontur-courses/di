using System.Collections.Generic;
using TagsCloudContainer.Infrastructure.Common;

namespace TagsCloudContainer
{
    public interface IWordsSelector
    {
        IEnumerable<LayoutWord> Select();
    }
}