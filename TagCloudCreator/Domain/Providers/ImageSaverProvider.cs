using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class ImageSaverProvider : IImageSaverProvider
{
    private readonly Dictionary<string, IImageSaver> _wordsFileReaders;
    private readonly IImagePathProvider _wordsPathProvider;

    public ImageSaverProvider(
        IEnumerable<IImageSaver> wordsFileReaders,
        IImagePathProvider wordsPathProvider
    )
    {
        _wordsFileReaders = wordsFileReaders.ToDictionary(saver => saver.SupportedExtension);
        _wordsPathProvider = wordsPathProvider;
    }

    public IReadOnlyCollection<string> SupportedExtensions => _wordsFileReaders.Keys;

    public IImageSaver GetSaver()
    {
        var imageExtension = Path.GetExtension(_wordsPathProvider.ImagePath);
        if (_wordsFileReaders.TryGetValue(imageExtension, out var result))
            return result;
        throw new ArgumentException($"No saver for extension: {imageExtension}");
    }
}