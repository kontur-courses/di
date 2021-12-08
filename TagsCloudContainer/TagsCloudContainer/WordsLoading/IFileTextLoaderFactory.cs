namespace TagsCloudContainer.WordsLoading
{
    public interface IFileTextLoaderFactory
    {
        IFileTextLoader GetByFileName(string filename);
    }
}