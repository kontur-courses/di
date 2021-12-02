namespace TagsCloudVisualization.Abstractions;

public interface ITagPacker
{
    IEnumerable<ITag> GetTags();
}