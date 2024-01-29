using TagsCloudCore.Common.Enums;

namespace TagsCloudCore.WordProcessing.WordInput;

public interface IWordProvider
{
    public string[] GetWords(string resourceLocation);

    WordProviderType Info { get; }

    public bool Match(WordProviderType info) => info == Info;
}