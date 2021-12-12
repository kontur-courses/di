using TagsCloudContainer.Common;

namespace TagsCloudContainer.Preprocessors
{
    [State(State.Active)]
    public class ShortTagsFilter : PreprocessorFilter
    {
        protected override bool IsRelevantTag(SimpleTag tag)
            => tag.Word.Length > 2;
    }
}