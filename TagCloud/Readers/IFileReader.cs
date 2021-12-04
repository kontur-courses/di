namespace TagCloud.Readers
{
    public interface IFileReader
    {
        string[] ReadFile(string filename);
    }
}
