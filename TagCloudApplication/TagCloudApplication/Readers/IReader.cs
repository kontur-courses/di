namespace TagCloudApplication.Readers
{
    public interface IReader
    {
        string GetText(string fileName);
    }
}