namespace TagCloudApp.Abstractions;

public interface IWordsLoader
{
    IEnumerable<string> LoadWords();
}