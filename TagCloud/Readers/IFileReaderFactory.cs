namespace TagCloud.Readers
{
    public interface IFileReaderFactory
    {
        IFileReader Create(string fileExtension);
    }
}
