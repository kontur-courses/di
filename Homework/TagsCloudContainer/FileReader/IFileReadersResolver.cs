namespace TagsCloudContainer.FileReader
{
    public interface IFileReadersResolver
    {
        IFileReader Get(string path);
    }
}