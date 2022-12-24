using TagCloudContainer.Core.Models;

namespace TagCloudContainer.Core.Interfaces;

public interface  IWordsReader
{
    public IEnumerable<Word> GetWordsFromFile(string fileName);
}