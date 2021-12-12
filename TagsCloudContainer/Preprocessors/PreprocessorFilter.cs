using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public abstract class PreprocessorFilter : IPreprocessor
    {
        public IEnumerable<SimpleTag> Process(IEnumerable<SimpleTag> tags)
            => tags.Where(IsRelevantTag);

        protected abstract bool IsRelevantTag(SimpleTag tag);
    }
}