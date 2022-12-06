using TagCloudCreator.Infrastructure;

namespace TagCloudCreator.Interfaces;

public interface IWordsInfoParser
{
    IEnumerable<WordInfo> GetWordsInfo();
}