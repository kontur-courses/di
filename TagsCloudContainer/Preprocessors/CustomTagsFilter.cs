using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class CustomTagsFilter : PreprocessorFilter
    {
        public delegate bool RelevantTag(SimpleTag tag);
        private readonly RelevantTag relevantTag;

        public CustomTagsFilter(RelevantTag relevantTag)
        {
            this.relevantTag = relevantTag;
        }

        protected override bool IsRelevantTag(SimpleTag tag)
            => relevantTag(tag);
    }
}