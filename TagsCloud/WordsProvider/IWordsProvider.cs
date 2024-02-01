using TagsCloud.WordValidators;

namespace TagsCloud.TextReaders;

public interface IWordsProvider
{
    public Dictionary<string, int> GetWords();
}