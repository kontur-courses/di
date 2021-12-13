using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class CentredSorterPreprocessor : IPreprocessor
    {
        public static State State { get; set; } = State.Inactive;

        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
            => tags.OrderByDescending(t => t.Count)
                .ThenByDescending(t => t.Word.Length);
    }
}