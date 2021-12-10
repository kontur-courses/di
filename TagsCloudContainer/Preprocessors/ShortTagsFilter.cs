using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class ShortTagsFilter : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
        {
            return tags.Where(IsRelevantTag);
        }

        private bool IsRelevantTag(SimpleTag tag)
            => tag.Word.Length > 2;
    }
}