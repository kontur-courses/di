using System.Collections.Generic;

namespace TagsCloudContainer.TextPreparation
{
    public interface IWordsReader
    {
        List<string> ReadAllWords(string fileContent);
    }
}