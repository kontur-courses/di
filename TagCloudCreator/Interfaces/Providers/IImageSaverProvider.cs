namespace TagCloudCreator.Interfaces.Providers;

public interface IImageSaverProvider
{
    IEnumerable<string> SupportedExtensions { get; }
    IImageSaver GetSaver();
}