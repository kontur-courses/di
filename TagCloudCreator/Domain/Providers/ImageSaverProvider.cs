using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class ImageSaverProvider : IImageSaverProvider
{
    private readonly Dictionary<string, IImageSaver> _wordsFileReaders;
    private readonly IImagePathSettingsProvider _pathSettingsProvider;

    public ImageSaverProvider(
        IEnumerable<IImageSaver> wordsFileReaders,
        IImagePathSettingsProvider pathSettingsProvider
    )
    {
        _wordsFileReaders = wordsFileReaders.ToDictionary(saver => saver.SupportedExtension);
        _pathSettingsProvider = pathSettingsProvider;
    }

    public IEnumerable<string> SupportedExtensions => _wordsFileReaders.Keys;

    public IImageSaver GetSaver()
    {
        var imageExtension = Path.GetExtension(_pathSettingsProvider.GetImagePathSettings().ImagePath);
        if (_wordsFileReaders.TryGetValue(imageExtension, out var result))
            return result;
        throw new InvalidOperationException($"No saver for extension: {imageExtension}");
    }
}