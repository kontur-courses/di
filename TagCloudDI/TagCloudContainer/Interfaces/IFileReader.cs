namespace TagCloudContainer.Interfaces
{
    public interface IFileReader
    {
        string TxtRead(string path);
        string DocRead(string path);
    }
}
