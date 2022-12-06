using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain.Providers;

public class WordsFileReaderProvider : IWordsFileReaderProvider
{
    private readonly Dictionary<string, IWordsFileReader> _wordsFileReaders;
    private readonly IWordsPathProvider _wordsPathProvider;

    public WordsFileReaderProvider(
        IEnumerable<IWordsFileReader> wordsFileReaders,
        IWordsPathProvider wordsPathProvider)
    {
        _wordsFileReaders = wordsFileReaders.ToDictionary(reader => reader.SupportedExtension);
        _wordsPathProvider = wordsPathProvider;
    }

    public IReadOnlyCollection<string> SupportedExtensions => _wordsFileReaders.Keys;

    public IWordsFileReader GetReader()
    {
        var wordsFileExtension = Path.GetExtension(_wordsPathProvider.WordsPath);
        if (_wordsFileReaders.TryGetValue(wordsFileExtension, out var result))
            return result;
        throw new ArgumentException($"No reader for extension: {wordsFileExtension}");
    }
}