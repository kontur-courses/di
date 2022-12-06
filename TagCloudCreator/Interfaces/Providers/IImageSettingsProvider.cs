using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Domain;

namespace TagCloudCreator.Interfaces.Providers;

public interface IImageSettingsProvider
{
    ImageSettings ImageSettings { get; }
}