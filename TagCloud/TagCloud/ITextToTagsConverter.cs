namespace TagCloud;

public interface ITextToTagsConverter
{
    public Dictionary<string, int> GetTags();
}