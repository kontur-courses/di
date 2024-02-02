
using TagsCloudContainer.Utility;

namespace TagsCloudContainer.Interfaces
{
    public interface IFileReader
    {
        Result<IEnumerable<string>> ReadWords(string filePath);
    }
}
