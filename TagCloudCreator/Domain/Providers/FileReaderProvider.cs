using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class FileReaderProvider : IFileReaderProvider
{
    private readonly Dictionary<string, IFileReader> _wordsFileReaders;
    private readonly IWordsPathSettingsProvider _pathSettingsProvider;

    public FileReaderProvider(
        IEnumerable<IFileReader> wordsFileReaders,
        IWordsPathSettingsProvider pathSettingsProvider)
    {
        _wordsFileReaders = wordsFileReaders.ToDictionary(reader => reader.SupportedExtension);
        _pathSettingsProvider = pathSettingsProvider;
    }

    public IEnumerable<string> SupportedExtensions => _wordsFileReaders.Keys;

    public IFileReader GetReader()
    {
        var wordsFileExtension = Path.GetExtension(_pathSettingsProvider.GetWordsPathSettings().WordsPath);
        if (_wordsFileReaders.TryGetValue(wordsFileExtension, out var result))
            return result;
        throw new InvalidOperationException($"No reader for extension: {wordsFileExtension}");
    }
}