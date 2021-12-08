using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IFileReader
    {
        List<string> GetAllWords(string filePath);
    }
}