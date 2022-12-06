using TagCloudApp.Abstractions;
using TagCloudApp.Actions;
using TagCloudApp.Domain;

namespace TagCloudApp;

public class AppSettings : IImageDirectoryProvider, IImageSettingsProvider
{
    public string ImagesDirectory { get; set; } = ".";
    public ImageSettings ImageSettings { get; set; } = new();
}