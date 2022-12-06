namespace TagCloudCreator.Interfaces.Providers;

public interface IImageSaverProvider
{
    IReadOnlyCollection<string> SupportedExtensions { get; }
    IImageSaver GetSaver();
}