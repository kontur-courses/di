using TagCloudApp.Domain;

namespace TagCloudApp.Abstractions;

public interface IWordsInfoProvider
{
    IReadOnlyCollection<WordInfo> WordInfos { get; }
}