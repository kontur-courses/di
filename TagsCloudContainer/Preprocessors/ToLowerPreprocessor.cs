using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class ToLowerPreprocessor : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
        {
            return tags.Select(Normalize);
        }

        private SimpleTag Normalize(SimpleTag tag)
        {
            var processedWord = tag.Word.ToLower();
            return new SimpleTag(processedWord, tag.Count);
        }
    }
}