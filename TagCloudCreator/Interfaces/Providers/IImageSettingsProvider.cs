using TagCloudCreator.Domain.Settings;

namespace TagCloudCreator.Interfaces.Providers;

public interface IImageSettingsProvider
{
    ImageSettings GetImageSettings();
}