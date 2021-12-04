namespace TagsCloudContainer.WordsLoading
{
    public interface IFileLoaderFactory
    {
        IWordsLoader GetByFileName(string filename);
    }

    public class FileLoaderFactory : IFileLoaderFactory
    {
        public IWordsLoader GetByFileName(string filename)
        {
            return new NewLineWordsLoader();
        }
    }
}