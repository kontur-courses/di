namespace TagCloudContainer.Readers
{
    public interface IFileReader
    {
        string TxtRead(string path);
        string DocRead(string path);
    }
}
