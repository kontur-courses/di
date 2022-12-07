using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;

namespace TagCloudCreator.Domain.Providers;

public class ImageSaverProvider : IImageSaverProvider
{
    private readonly Dictionary<string, IImageSaver> _wordsFileReaders;
    private readonly IImagePathSettings _wordsPathSettings;

    public ImageSaverProvider(
        IEnumerable<IImageSaver> wordsFileReaders,
        IImagePathSettings wordsPathSettings
    )
    {
        _wordsFileReaders = wordsFileReaders.ToDictionary(saver => saver.SupportedExtension);
        _wordsPathSettings = wordsPathSettings;
    }

    public IEnumerable<string> SupportedExtensions => _wordsFileReaders.Keys;

    public IImageSaver GetSaver()
    {
        var imageExtension = Path.GetExtension(_wordsPathSettings.ImagePath);
        if (_wordsFileReaders.TryGetValue(imageExtension, out var result))
            return result;
        throw new ArgumentException($"No saver for extension: {imageExtension}");
    }
}