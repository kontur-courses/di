namespace TagsCloudApp.WordsLoading
{
    public interface IFileTextLoaderResolver
    {
        IFileTextLoader GetFileTextLoader(FileType type);
    }
}