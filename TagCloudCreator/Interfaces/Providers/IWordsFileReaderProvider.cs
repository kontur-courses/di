namespace TagCloudCreator.Interfaces.Providers;

public interface IWordsFileReaderProvider
{
    IReadOnlyCollection<string> SupportedExtensions { get; }
    IWordsFileReader GetReader();
}