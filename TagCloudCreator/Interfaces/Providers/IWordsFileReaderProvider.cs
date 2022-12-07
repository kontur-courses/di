namespace TagCloudCreator.Interfaces.Providers;

public interface IWordsFileReaderProvider
{
    IEnumerable<string> SupportedExtensions { get; }
    IWordsFileReader GetReader();
}