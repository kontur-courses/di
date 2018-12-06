namespace TagCloud.Utility.Models.Tag.Container
{
    public interface ITagContainerReader
    {
        ITagContainer ReadTagsContainer(string text);
    }
}