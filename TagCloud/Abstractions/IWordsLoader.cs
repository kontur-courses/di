namespace TagCloud.Abstractions;

public interface IWordsLoader
{
    IEnumerable<string> Load();
}