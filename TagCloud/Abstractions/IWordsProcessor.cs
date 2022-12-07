namespace TagCloud.Abstractions;

public interface IWordsProcessor
{
    IEnumerable<string> Process(IEnumerable<string> words);
}