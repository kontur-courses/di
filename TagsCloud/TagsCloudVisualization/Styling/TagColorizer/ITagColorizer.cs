namespace TagsCloudVisualization.Styling.TagColorizer
{
    public interface ITagColorizer
    {
        //todo tests
        string GetTagColor(string[] tagColors, Tag tag);
    }
}