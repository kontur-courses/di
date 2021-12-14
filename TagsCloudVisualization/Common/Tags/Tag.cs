namespace TagsCloudVisualization.Common.Tags
{
    public class Tag
    {
        public string Text { get; }
        
        public TagStyle Style { get; }

        public Tag(string text, TagStyle style)
        {
            Text = text;
            Style = style;
        }
    }
}