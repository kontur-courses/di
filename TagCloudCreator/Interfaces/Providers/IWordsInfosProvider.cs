using TagCloudCreator.Infrastructure;
using TagCloudCreator.Domain;

namespace TagCloudCreator.Interfaces.Providers;

public interface IWordsInfosProvider
{
    IEnumerable<WordInfo> WordsInfos { get; }
}