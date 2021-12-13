using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    public class ShortTagsFilter : PreprocessorFilter
    {
        public static State State { get; set; } = State.Active;
        protected override bool IsRelevantTag(SimpleTag tag)
            => tag.Word.Length > 2;
    }
}