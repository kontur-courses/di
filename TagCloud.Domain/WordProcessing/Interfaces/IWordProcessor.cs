using TagCloud.Domain.WordEntities;

namespace TagCloud.Domain.WordProcessing.Interfaces;

public interface IWordProcessor
{
    public WordsWithCount GetClearWords(IEnumerable<string> words);
}