using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class CustomTagsFilter : PreprocessorFilter
    {
        public static State State { get; set; } = State.Inactive;

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