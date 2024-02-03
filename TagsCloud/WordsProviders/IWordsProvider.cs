using TagsCloud.WordValidators;

namespace TagsCloud.WordsProviders;

public interface IWordsProvider
{
    public Dictionary<string, int> GetWords();
}