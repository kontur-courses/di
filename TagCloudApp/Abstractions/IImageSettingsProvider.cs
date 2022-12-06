using TagCloudApp.Domain;

namespace TagCloudApp.Abstractions;

public interface IImageSettingsProvider
{
    ImageSettings ImageSettings { get; }
}