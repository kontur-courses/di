namespace TagCloudApp.Abstractions;

public interface IWordsFileReader
{
    public string SupportedExtension { get; }
    IEnumerable<string> GetWords();
}