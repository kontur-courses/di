namespace TagCloudApp.Abstractions;

public interface IImageSaverProvider
{
    IReadOnlyCollection<string> SupportedExtensions { get; }
    IImageSaver GetSaver();
}