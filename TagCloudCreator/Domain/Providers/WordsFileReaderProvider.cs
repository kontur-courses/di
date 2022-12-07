using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;
using TagCloudCreator.Interfaces.Settings;

namespace TagCloudCreator.Domain.Providers;

public class WordsFileReaderProvider : IWordsFileReaderProvider
{
    private readonly Dictionary<string, IWordsFileReader> _wordsFileReaders;
    private readonly IWordsPathSettings _wordsPathSettings;

    public WordsFileReaderProvider(
        IEnumerable<IWordsFileReader> wordsFileReaders,
        IWordsPathSettings wordsPathSettings)
    {
        _wordsFileReaders = wordsFileReaders.ToDictionary(reader => reader.SupportedExtension);
        _wordsPathSettings = wordsPathSettings;
    }

    public IReadOnlyCollection<string> SupportedExtensions => _wordsFileReaders.Keys;

    public IWordsFileReader GetReader()
    {
        var wordsFileExtension = Path.GetExtension(_wordsPathSettings.WordsPath);
        if (_wordsFileReaders.TryGetValue(wordsFileExtension, out var result))
            return result;
        throw new ArgumentException($"No reader for extension: {wordsFileExtension}");
    }
}