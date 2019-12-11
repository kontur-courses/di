namespace TagsCloudVisualization.Styling.TagColorizer
{
    public interface ITagColorizer
    {
        string GetTagColor(string[] tagColors, Tag tag);
    }
}