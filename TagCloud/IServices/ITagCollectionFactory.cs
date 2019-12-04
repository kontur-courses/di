namespace TagCloud.IServices
{
    public interface ITagCollectionFactory
    {
        TagCollection Create(string path);
    }
}