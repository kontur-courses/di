using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreatorExtensions.WordsFileReaders;

public class TxtWordsFileReader : IWordsFileReader
{
    private readonly IWordsPathProvider _pathProvider;

    public TxtWordsFileReader(IWordsPathProvider pathProvider)
    {
        _pathProvider = pathProvider;
    }

    public string SupportedExtension => ".txt";

    public IEnumerable<string> GetWords()
    {
        var dir = _pathProvider.WordsPath;
        return File.ReadLines(dir);
    }
}