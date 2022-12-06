using TagCloudCreator.Infrastructure;
using TagCloudCreator.Domain;

namespace TagCloudCreator.Interfaces;

public interface IWordsInfoParser
{
    IEnumerable<WordInfo> GetWordsInfos();
}