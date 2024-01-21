
namespace TagsCloudContainer.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> ReadWords(string filePath);
    }
}
