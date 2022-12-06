namespace TagCloudCreator.Interfaces;

public interface IWordsFileReader
{
    public string SupportedExtension { get; }
    IEnumerable<string> GetWords();
}