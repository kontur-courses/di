using System.Collections.Generic;

namespace TagsCloudContainer.TextPreparation
{
    public interface IFileReader
    {
        List<string> GetAllWords(string filePath);
    }
}