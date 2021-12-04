namespace TagCloudContainer.Infrastructure.FileReader
{
    public interface IFileReader
    {
        IEnumerable<string> GetLines(string filePath);
    }
}
