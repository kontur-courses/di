using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class SorterPreprocessor : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
            => tags.OrderBy(t => t.Count)
                .ThenBy(t => t.Word.Length);
    }
}