using TagCloudApp.Domain;

namespace TagCloudApp;

public interface IImageSettingsProvider
{
    ImageSettings ImageSettings { get; }
}