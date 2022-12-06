using TagCloudApp.Domain;

namespace TagCloudApp.Abstractions;

public interface IWordsInfoProvider
{
    IEnumerable<WordInfo> WordInfos { get; }
}