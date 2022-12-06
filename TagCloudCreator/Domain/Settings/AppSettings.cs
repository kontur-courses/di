using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Settings;

public class AppSettings : IImagePathProvider, IWordsPathProvider, IImageSettingsProvider
{
    public string ImagePath { get; set; } = null!;
    public string WordsPath { get; set; } = null!;
    public ImageSettings ImageSettings { get; init; } = null!;
}