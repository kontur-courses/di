using TagCloudApp.Domain;

namespace TagCloudApp.Abstractions;

public interface IWordsSelector
{
    IEnumerable<WordInfo> GetWordsInfos();
}