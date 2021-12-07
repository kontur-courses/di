namespace TagsCloudContainer.WordsLoading
{
    public interface IFileLoaderFactory
    {
        IWordsLoader GetByFileName(string filename);
    }
}