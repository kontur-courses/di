using TagsCloud.WordValidators;

namespace TagsCloud.TextReaders;

public interface ITextReader
{
    public Dictionary<string,int> GetWords();
}