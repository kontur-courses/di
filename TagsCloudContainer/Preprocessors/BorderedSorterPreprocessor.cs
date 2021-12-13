using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class BorderedSorterPreprocessor : IPreprocessor
    {
        public static State State { get; set; } = State.Inactive;

        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
            => tags.OrderBy(t => t.Count)
                .ThenBy(t => t.Word.Length);
    }
}