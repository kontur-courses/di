using TagCloudCreator.Interfaces.Settings;

namespace TagCloudCreator.Interfaces.Providers;

public interface IWordsPathSettingsProvider
{
    IWordsPathSettings GetWordsPathSettings();
}