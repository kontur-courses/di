using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreatorExtensions.WordsFileReaders;

public class TxtWordsFileReader : IWordsFileReader
{
    private readonly IWordsPathSettingsProvider _pathSettingsProvider;

    public TxtWordsFileReader(IWordsPathSettingsProvider pathSettingsProvider)
    {
        _pathSettingsProvider = pathSettingsProvider;
    }

    public string SupportedExtension => ".txt";

    public IEnumerable<string> GetWords()
    {
        var dir = _pathSettingsProvider.GetWordsPathSettings().WordsPath;
        return File.ReadLines(dir);
    }
}