using System.Collections.Generic;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class CustomBoringTagsFilter : PreprocessorFilter
    {
        private readonly HashSet<string> boringWords;

        public CustomBoringTagsFilter(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }

        protected override bool IsRelevantTag(SimpleTag tag)
            => !boringWords.Contains(tag.Word);
    }
}