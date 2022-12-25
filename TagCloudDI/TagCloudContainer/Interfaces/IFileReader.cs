namespace TagCloudContainer.Interfaces
{
    public interface IFileReader
    {
        string ReadFile(string path);
        string TxtRead(string path);
        string DocRead(string path);
    }
}
