using TagCloudContainer.Additions.Models;

namespace TagCloudContainer.Additions.Interfaces;

public interface  IWordsReader
{
    public IEnumerable<Word> GetWordsFromFile(string fileName);
}