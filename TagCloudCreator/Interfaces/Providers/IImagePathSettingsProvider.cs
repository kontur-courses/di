using TagCloudCreator.Interfaces.Settings;

namespace TagCloudCreator.Interfaces.Providers;

public interface IImagePathSettingsProvider
{
    IImagePathSettings GetImagePathSettings();
}