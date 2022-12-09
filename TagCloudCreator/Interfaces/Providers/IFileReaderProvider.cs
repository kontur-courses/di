namespace TagCloudCreator.Interfaces.Providers;

public interface IFileReaderProvider
{
    IEnumerable<string> SupportedExtensions { get; }
    IFileReader GetReader();
}