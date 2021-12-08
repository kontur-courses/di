namespace TagsCloudContainer.WordsLoading
{
    public class FileTextLoaderFactory : IFileTextLoaderFactory
    {
        public IFileTextLoader GetByFileName(string filename)
        {
            return new TxtFileTextLoader();
        }
    }
}