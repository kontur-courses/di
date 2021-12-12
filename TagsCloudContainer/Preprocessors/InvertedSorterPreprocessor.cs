using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    [State(State.Inactive)]
    public class InvertedSorterPreprocessor : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
            => tags.OrderByDescending(t => t.Count)
                .ThenByDescending(t => t.Word.Length);
    }
}