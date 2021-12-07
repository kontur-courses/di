namespace TagsCloudContainer.WordsLoading
{
    public class FileLoaderFactory : IFileLoaderFactory
    {
        public IWordsLoader GetByFileName(string filename)
        {
            return new NewLineWordsLoader();
        }
    }
}