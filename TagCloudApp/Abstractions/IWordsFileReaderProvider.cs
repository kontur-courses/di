namespace TagCloudApp.Abstractions;

public interface IWordsFileReaderProvider
{
    IReadOnlyCollection<string> SupportedExtensions { get; }
    IWordsFileReader GetReader();
}