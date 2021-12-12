namespace TagsCloudVisualization.Common.Tags
{
    public class StyledTag
    {
        public Tag Tag { get; }
        public ITagStyle Style { get; }

        public StyledTag(Tag tag, ITagStyle style)
        {
            Tag = tag;
            Style = style;
        }
    }
}